const path = require("path");
// const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const HtmlWebpackPlugin = require("html-webpack-plugin");
const webpack = require('webpack'); //to access built-in plugins
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const ForkTsCheckerWebpackPlugin = require("fork-ts-checker-webpack-plugin")

const ModuleFederationPlugin = require("webpack").container.ModuleFederationPlugin;

module.exports = {
    
    entry: "./src/index.tsx",
    target: "web",
    mode: "development",//"production",
    devServer: {
        contentBase: path.join(__dirname, "dist"),
        port: 3002,
        historyApiFallback: true,
        headers: {
            "Access-Control-Allow-Origin": "*",
            "Access-Control-Allow-Methods": "GET, POST, PUT, DELETE, PATCH, OPTIONS",
            "Access-Control-Allow-Headers": "X-Requested-With, content-type, Authorization"
          }
    },
    output: {
        path: path.resolve(__dirname, "build"),
        filename: "bundle.js",
    },
    resolve: {
        extensions: [".js", ".jsx", ".json", ".ts", ".tsx"],
        alias: {
            react: path.resolve(__dirname, './node_modules/react'),
        },
    },
    module: {
        rules: [
            {
                test: /\.(js|jsx|)$/,
                loader: "babel-loader"
            },
            {
                test: /\.(ts|tsx)$/,
                loader: "awesome-typescript-loader",

            },
            {
                enforce: "pre",
                test: /\.js$/,
                loader: "source-map-loader",

            },
            {
                test: /\.css$/,
                loader: "css-loader",
                options: {
                    modules: true,
                    importLoaders: 1
                }

            },
            // {
            //     test: /\.(scss|css)$/,
            //     use: ['style-loader','css-loader','sass-loader']
            // },
            {
                test: /\.s[ac]ss$/,
                use: [
                    "style-loader",
                    {
                        loader: "css-loader",
                    },
                    "resolve-url-loader",
                    {
                        loader: "sass-loader",
                        options: {
                            sourceMap: true
                        }
                    }
                ]
            },
            // {
            //     test: /\.svg$/,
            //     loader: 'svg-inline-loader'
            // },
            {
                test: /\.(png|svg|jpg|jpeg|gif)$/i,
                type: 'asset',
            },

            {
                test: /\.(woff|woff2|eot|ttf|otf)$/i,
                type: 'asset/resource',
            },
            // {
            //     test: /\.(png|jpe?g|gif)$/i,
            //     use: [
            //       {
            //         loader: 'file-loader',
            //         options: {
            //             limit: 10000,
            //             name: `assets/images/[name].[hash:8].[ext]`, //Path will be assets or image path
            //           }
            //       },
            //     ],
            //   },
            //   {
            //     test:  [/\.bmp$/, /\.gif$/, /\.jpe?g$/, /\.png$/, /\.svg$/],
            //     use: [
            //         {
            //           loader: require.resolve('url-loader'), //'url-loader',
            //           options: {
            //             limit: 10000,
            //             name: `assets/images/[name].[hash:8].[ext]`, //Path will be assets or image path
            //           }
            //         }
            //      ]
            //   },

            // {
            //     test: /\.(png|jpg|gif)$/i,
            //     use: [
            //         {
            //             loader: 'url-loader',
            //             options: {
            //                 limit: 8192,
            //             },
            //         },
            //     ],
            // }
        ],
    },
    plugins: [
        new ModuleFederationPlugin({
            name: "loanportalapp",
            filename: "remoteentry.js",
            exposes: {
                "./loanportal": "./src/App"
            },
            shared: {
                "react": { "singleton": true },
				"react-dom": { "singleton": true }
            },
        }),
        new webpack.ProgressPlugin(),
        new HtmlWebpackPlugin({
            template: path.resolve(__dirname, "public", "index.html"),
        }),
        new MiniCssExtractPlugin({
            filename: "./src/style.css",
            chunkFilename: "[id].css"
        }),
        new ForkTsCheckerWebpackPlugin({
            async: false
        })
    ],
};