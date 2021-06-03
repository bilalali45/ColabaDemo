const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const TerserPlugin = require("terser-webpack-plugin");
const webpack = require('webpack'); //to access built-in plugins
const ForkTsCheckerWebpackPlugin = require("fork-ts-checker-webpack-plugin")


const ModuleFederationPlugin = require("webpack").container.ModuleFederationPlugin;

module.exports = {
    entry: {
        main: './src/index.tsx',
      },
    // entry: "./src/index.tsx",
    target: "web",
    output: {
        path: path.resolve(__dirname, '..', "build"),
        filename: "static/js/[name].bundle.js",
        assetModuleFilename: 'static/images/[name][ext][query]'
    },
    resolve: {
        extensions: [".js", ".jsx", ".json", ".ts", ".tsx", '.scss', '.css'],
    },
    optimization: {
        minimize: true,
        minimizer: [new TerserPlugin({
            extractComments: false,
        })],
    },
    module: {
        rules: [
            {
                test: /\.(ts|tsx)$/,
                exclude: /node_modules/,
                loader: "awesome-typescript-loader",
            },
            {
                test: /\.(scss|css)$/,
                use: [
                    {
                        loader: MiniCssExtractPlugin.loader,
                        options: {
                            publicPath: '/'
                        }
                    },
                    'css-loader',
                    'sass-loader'
                ],
            },
            {
                test: /\.(png|svg|jpg|jpeg|gif)$/i,
                type: 'asset',
            },
            {
                test: /\.(woff(2)?|ttf|eot|svg)(\?v=\d+\.\d+\.\d+)?$/,
                type: 'asset/resource',
            }
        ],
    },
    plugins: [
        new ModuleFederationPlugin({
            name: "docmanagerapp",
            filename: "remoteEntry.js",
            exposes: {
                "./docmanager": "./src/App"
            },
            shared: {
                "react": { "singleton": true },
                "react-dom": { "singleton": true }
            },
        }),
        new HtmlWebpackPlugin({
            template: path.resolve(__dirname, "..", "public", "index.html"),
        }),

        new MiniCssExtractPlugin(
            {
                filename: "static/css/styles.css",
            }
        ),
        // new MiniCssExtractPlugin({
        //     filename: "./src/style.css",
        //     chunkFilename: "[id].css"
        // }),
        new CleanWebpackPlugin(),
        new webpack.ProgressPlugin(),
        new ForkTsCheckerWebpackPlugin({
            async: false
        })
    ],
};