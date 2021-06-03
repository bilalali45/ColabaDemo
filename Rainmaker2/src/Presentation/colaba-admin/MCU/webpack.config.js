const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const webpack = require('webpack'); //to access built-in plugins
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const ForkTsCheckerWebpackPlugin = require("fork-ts-checker-webpack-plugin")
const InterpolateHtmlPlugin = require('react-dev-utils/InterpolateHtmlPlugin');

const ModuleFederationPlugin = require("webpack").container.ModuleFederationPlugin;

module.exports = {
    
    entry: "./src/index.tsx",
    target: "web",
    mode: "development",//"production",
    devServer: {
        contentBase: path.join(__dirname, "dist"),
        port: 3002,
        historyApiFallback: true,
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
            {
                test: /\.svg$/,
                loader: 'svg-inline-loader'
            },
            {
                test: /\.(png|svg|jpg|jpeg|gif)$/i,
                type: 'asset/resource',
            },
            {
                test: /\.(png|jpe?g|gif)$/i,
                use: [
                  {
                    loader: 'file-loader',
                  },
                ],
              }
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
            name: "mcuapp",
            filename: "remoteEntry.js",
            exposes: {
                "./mcu": "./src/App",
            },
            shared: {
                react: {
                    import: "react", // the "react" package will be used a provided and fallback module
                    shareKey: "react", // under this name the shared module will be placed in the share scope
                    shareScope: "default", // share scope with this name will be used
                    singleton: true, // only a single version of the shared module is allowed
                },
                "react-dom": {
                    singleton: true, // only a single version of the shared module is allowed
                },
            },
        }),
        new webpack.ProgressPlugin(),
        new HtmlWebpackPlugin({
            template: path.resolve(__dirname, "public", "index.html"),
        }),
        new MiniCssExtractPlugin({
            filename: "./src/yourfile.css",
            chunkFilename: "[id].css"
        }),
        // new InterpolateHtmlPlugin(HtmlWebpackPlugin,{
        //     PUBLIC_URL: env.PUBLIC_URL,
        //     // You can pass any key-value pairs, this was just an example.
        //     // WHATEVER: 42 will replace %WHATEVER% with 42 in index.html.
        //   }),
        new ForkTsCheckerWebpackPlugin({
            async: false
        })
    ],
};