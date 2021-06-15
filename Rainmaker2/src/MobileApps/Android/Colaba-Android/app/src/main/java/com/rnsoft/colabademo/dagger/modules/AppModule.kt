package com.rnsoft.colabademo

import android.content.Context
import android.content.SharedPreferences
import android.security.keystore.KeyGenParameterSpec
import android.security.keystore.KeyProperties
import androidx.security.crypto.EncryptedSharedPreferences
import androidx.security.crypto.MasterKey
import com.bumptech.glide.Glide
import com.bumptech.glide.RequestManager
import com.bumptech.glide.request.RequestOptions
import com.franmontiel.persistentcookiejar.PersistentCookieJar
import com.franmontiel.persistentcookiejar.cache.SetCookieCache
import com.franmontiel.persistentcookiejar.persistence.SharedPrefsCookiePersistor
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.android.qualifiers.ApplicationContext
import dagger.hilt.components.SingletonComponent
import okhttp3.OkHttpClient
import okhttp3.logging.HttpLoggingInterceptor
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.net.CookieManager
import java.net.CookiePolicy
import java.util.concurrent.TimeUnit
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
class AppModule {



        @Singleton
        @Provides
        fun provideSharedPreferences(  @ApplicationContext context: Context): SharedPreferences {
            //return context.getSharedPreferences(MyAppConfigConstant.APP_PREFERENCES, Context.MODE_PRIVATE)
            val spec = KeyGenParameterSpec.Builder(
                MasterKey.DEFAULT_MASTER_KEY_ALIAS,
                KeyProperties.PURPOSE_ENCRYPT or KeyProperties.PURPOSE_DECRYPT
            )
                .setBlockModes(KeyProperties.BLOCK_MODE_GCM)
                .setEncryptionPaddings(KeyProperties.ENCRYPTION_PADDING_NONE)
                .setKeySize(256)
                .build()

            val masterKey = MasterKey.Builder(context)
                .setKeyGenParameterSpec(spec)
                .build()

             return EncryptedSharedPreferences.create(
                context,
                 ColabaConstant.APP_PREFERENCES,
                masterKey, // masterKey created above
                EncryptedSharedPreferences.PrefKeyEncryptionScheme.AES256_SIV,
                EncryptedSharedPreferences.PrefValueEncryptionScheme.AES256_GCM)
        }


        @Singleton
        @Provides
        fun provideSharedPrefsEditor( sharedPreferences: SharedPreferences): SharedPreferences.Editor {
            return sharedPreferences.edit()
        }

        @Singleton
        @Provides
        fun provideGlideInstance(@ApplicationContext context: Context,  requestOptions: RequestOptions): RequestManager {
            return Glide.with(context)
                .setDefaultRequestOptions(requestOptions)
        }


        @Provides
        @Singleton
        //fun provideOkHttp(tokenAuthenticator: TokenAuthenticator): OkHttpClient {
        fun provideOkHttp( @ApplicationContext context: Context, tokenAuthenticator : TokenAuthenticator): OkHttpClient {
            //val interceptor = HttpLoggingInterceptor()
            //interceptor.setLevel(HttpLoggingInterceptor.Level.BODY)
            //val newLoginInterceptor = NewLoginInterceptor(HttpLoggingInterceptor.Logger.DEFAULT)
            //newLoginInterceptor.setL

            val httpLoggingInterceptor = HttpLoggingInterceptor()
            httpLoggingInterceptor.apply {
                httpLoggingInterceptor.level = HttpLoggingInterceptor.Level.BODY
            }

            val testCookieJar =
                PersistentCookieJar(SetCookieCache(), SharedPrefsCookiePersistor(context))


           // val cookieHandler = CookieManager( PersistentCookieStore(ctx), CookiePolicy.ACCEPT_ALL )

            //val cookieHandler = CookieManager( null, CookiePolicy.ACCEPT_ALL )

            return OkHttpClient.Builder()
                .authenticator(tokenAuthenticator)
                .retryOnConnectionFailure(true)
                //.addInterceptor(LoggingInterceptor())
                //.addInterceptor(interceptor)
                .addNetworkInterceptor(httpLoggingInterceptor)
                .cookieJar(testCookieJar)
                .connectTimeout(60,TimeUnit.SECONDS).readTimeout(60, TimeUnit.SECONDS)
                .writeTimeout(60, TimeUnit.SECONDS)
                .build()
        }




        @Provides
        @Singleton
        fun provideTokenAuthenticator(sharedPreferences: SharedPreferences): TokenAuthenticator {
            return TokenAuthenticator(sharedPreferences)
        }


        @Provides
        @Singleton
        fun retrofit(okHttpClient: OkHttpClient): Retrofit {
            return Retrofit.Builder()
                .baseUrl(ColabaConstant.BASE_URL)
                .client(okHttpClient)
                .addConverterFactory(GsonConverterFactory.create())
                .build()
        }

        @Provides
        @Singleton
        fun appServices(retrofit: Retrofit): ServerApi = retrofit.create(ServerApi::class.java)


        @Provides
        @Singleton
        fun provideLoginDataSource(serverApi: ServerApi): SignUpFlowDataSource {
                return SignUpFlowDataSource(serverApi)
        }

        @Provides
        @Singleton

        fun provideLoginRepo(dataSource: SignUpFlowDataSource, spEditor: SharedPreferences.Editor, @ApplicationContext applicationContext: Context): SignUpFlowRepository {
            return SignUpFlowRepository( dataSource,  spEditor, applicationContext )
        }

        @Provides
        @Singleton
        fun provideLoginViewModel(signUpFlowRepository: SignUpFlowRepository): SignUpFlowViewModel {
            return SignUpFlowViewModel( signUpFlowRepository )
        }



    /*
        @Provides
        @Singleton
        fun provideProductRepo( @ApplicationContext applicationContext: Application , loginRepository: LoginRepository): ProductRepository {
            return ProductRepository( applicationContext , loginRepository  )
        }

     */

    /*
      can not pass TestAPI, it is creating dependency cycle... Dagger is stopping it at compile time....
      @Provides
      @Singleton
      fun provideTokenAuthenticator(testApi: TestAPI, sharedPreferences: SharedPreferences): TokenAuthenticator {
          return TokenAuthenticator(testApi,sharedPreferences)
      }

       */
}



// If user credentials will be cached in local storage, it is recommended it be encrypted
// @see https://developer.android.com/training/articles/keystore


/*
val spec = KeyGenParameterSpec.Builder(
    MasterKey.DEFAULT_MASTER_KEY_ALIAS,
    KeyProperties.PURPOSE_ENCRYPT or KeyProperties.PURPOSE_DECRYPT
)
    .setBlockModes(KeyProperties.BLOCK_MODE_GCM)
    .setEncryptionPaddings(KeyProperties.ENCRYPTION_PADDING_NONE)
    .build()

val masterKey = MasterKey.Builder(applicationContext)
    .setKeyGenParameterSpec(spec)
    .build()

val sharedPreferences =EncryptedSharedPreferences.create(
   applicationContext,
    "FILE_NAME",
    masterKey, // masterKey created above
    EncryptedSharedPreferences.PrefKeyEncryptionScheme.AES256_SIV,
    EncryptedSharedPreferences.PrefValueEncryptionScheme.AES256_GCM);
 */

/*
Depreciated way...

// Although you can define your own key generation parameter specification, it's
// recommended that you use the value specified here.
val keyGenParameterSpec = MasterKeys.AES256_GCM_SPEC
val masterKeyAlias = MasterKeys.getOrCreate(keyGenParameterSpec)

val sharedPreferences = EncryptedSharedPreferences
    .create(
        "FILE_NAME",
        masterKeyAlias,
        applicationContext,
        EncryptedSharedPreferences.PrefKeyEncryptionScheme.AES256_SIV,
        EncryptedSharedPreferences.PrefValueEncryptionScheme.AES256_GCM
    )

val sharedPrefsEditor = sharedPreferences.edit()

 */