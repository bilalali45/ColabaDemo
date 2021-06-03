const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const TerserPlugin = require("terser-webpack-plugin");
const { merge } = require('webpack-merge');
const commonConfig = require('./webpack.common');
const CopyPlugin = require("copy-webpack-plugin");
const webpack = require('webpack');
const ModuleFederationPlugin = webpack.container.ModuleFederationPlugin;

const prodConfig = {
    mode: "production",
    output: {
        publicPath: './loanapplicationportal/'
    },
    plugins: [
        new webpack.EnvironmentPlugin({
            NODE_ENV: 'production', // use 'development' unless process.env.NODE_ENV is defined
        }),
        new webpack.DefinePlugin({
            'process.env': {
                NODE_ENV: JSON.stringify('development')
            }
        }),
        new CopyPlugin({
            patterns: [
                { from: "public", to: "public" },
                { from: path.resolve(__dirname, '..', '..', 'assets-hub', "fonts"), to: "static/css" },
            ],
        }),
        // new ModuleFederationPlugin({
        //     name: "loanportalapp",
        //     filename: "remoteentry.js",
        //     exposes: {
        //         "./loanportal": "./src/App"
        //     },
        //     shared: {
        //         "react": { "singleton": true },
        // 		"react-dom": { "singleton": true }
        //     },
        // }),
    ],
};

module.exports = merge(commonConfig, prodConfig);