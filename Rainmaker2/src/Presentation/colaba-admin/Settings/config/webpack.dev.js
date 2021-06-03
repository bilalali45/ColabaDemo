const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const TerserPlugin = require("terser-webpack-plugin");
const { merge } = require('webpack-merge');
const commonConfig = require('./webpack.common');



const ModuleFederationPlugin = require("webpack").container.ModuleFederationPlugin;

const devConfig = {
    mode: "development",
    devServer: {
        contentBase: path.join(__dirname, "dist"),
        port: 3004,
        historyApiFallback: true,
        headers: {
            "Access-Control-Allow-Origin": "*",
            "Access-Control-Allow-Methods": "GET, POST, PUT, DELETE, PATCH, OPTIONS",
            "Access-Control-Allow-Headers": "X-Requested-With, content-type, Authorization"
          }
    },
  
};

module.exports = merge(commonConfig, devConfig);