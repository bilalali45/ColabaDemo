const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const TerserPlugin = require("terser-webpack-plugin");

const ModuleFederationPlugin = require("webpack").container.ModuleFederationPlugin;

module.exports = {
    entry: "./src/index.tsx",
    target: "web",
    mode: "production",
    output: {
        path: path.resolve(__dirname, "build"),
        filename: "./static/js/[name].bundle.js",
        assetModuleFilename: 'static/images/[hash][ext][query]'
    },
    resolve: {
        extensions: [".js", ".jsx", ".json", ".ts", ".tsx", '.scss', '.css'],
        alias: {
            react: path.resolve(__dirname, './node_modules/react'),
        },
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
                type: 'asset',
            },
            {
                test: /\.(png|jpe?g|gif)$/i,
                use: [
                  {
                    loader: 'file-loader',
                  },
                ],
              }


        ],
    },
    plugins: [
        new ModuleFederationPlugin({
            name: "loanportalapp",
            exposes: {
                "./loanportal": "./src/App",
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
        new HtmlWebpackPlugin({
            template: './public/index.html',
        }),
        new MiniCssExtractPlugin(
            {
                filename: "static/css/styles.css",
            }
        ),
        new CleanWebpackPlugin(),
    ],
};