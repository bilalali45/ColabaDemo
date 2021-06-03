const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const TerserPlugin = require("terser-webpack-plugin");

const ModuleFederationPlugin = require("webpack").container.ModuleFederationPlugin;

module.exports = {
    entry: "./src/components/index.tsx",
    target: "web",
    mode: "production",
    output: {
        path: path.resolve(__dirname, "build"),
        filename: "static/js/[name].bundle.js",
        assetModuleFilename: 'static/images/[hash][ext][query]'
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
                    MiniCssExtractPlugin.loader,
                    'css-loader',
                    'sass-loader'
                ],
            },
            {
                test: /\.(png|svg|jpg|jpeg|gif)$/i,
                type: 'asset',
            },


        ],
    },
    plugins: [
        new ModuleFederationPlugin({
            name: "app1",
            remotes: {
                app2: "app2@http://localhost:3002/remoteEntry.js",
                app3: "app2@http://localhost:3002/remoteEntry.js",
            },
            shared: ["react", "react-dom"],
        }),
        new HtmlWebpackPlugin({
            template: path.resolve(__dirname, "src", "components", "index.html"),
        }),
        new MiniCssExtractPlugin(
            {
                filename: "static/css/styles.css",
            }
        ),
        new CleanWebpackPlugin(),
    ],
};