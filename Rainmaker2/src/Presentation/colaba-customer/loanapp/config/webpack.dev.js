const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const TerserPlugin = require("terser-webpack-plugin");
const { merge } = require('webpack-merge');
const commonConfig = require('./webpack.common');
const CopyPlugin = require("copy-webpack-plugin");


const ModuleFederationPlugin = require("webpack").container.ModuleFederationPlugin;

const devConfig = {
    mode: "development",
    devServer: {
        https: true,
        contentBase: path.join(__dirname, "dist"),
        port: 3003,
        historyApiFallback: true,
        headers: {
            "Access-Control-Allow-Origin": "*",
            "Access-Control-Allow-Methods": "GET, POST, PUT, DELETE, PATCH, OPTIONS",
            "Access-Control-Allow-Headers": "X-Requested-With, content-type, Authorization"
          }
    },
    plugins: [
        new CopyPlugin({
            patterns: [
                { from: path.resolve(__dirname, '..', '..', 'assets-hub', "fonts"), to: "static/css" },
            ],
        }), 
    ],
  
};

module.exports = merge(commonConfig, devConfig);