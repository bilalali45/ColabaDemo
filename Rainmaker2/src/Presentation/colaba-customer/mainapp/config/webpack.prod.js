const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const TerserPlugin = require("terser-webpack-plugin");
const { merge } = require('webpack-merge');
const commonConfig = require('./webpack.common');
const webpack = require('webpack');
const CopyPlugin = require("copy-webpack-plugin");

const ModuleFederationPlugin = require("webpack").container.ModuleFederationPlugin;

const prodConfig = {
    mode: "production",
    output: {
       publicPath: ''
    },
    plugins: [
        new webpack.EnvironmentPlugin({
            NODE_ENV: 'production', // use 'development' unless process.env.NODE_ENV is defined
        }),
		new CopyPlugin({
            patterns: [
                { from: "public", to: "public" },
                { from: path.resolve(__dirname, '..', '..', 'assets-hub', "fonts"), to: "static/css" },
            ],
        }),
        new ModuleFederationPlugin({
			name: "masterapp",
			remotes: {
                mfloanappportal: "loanapplicationportal@./loanapplicationportal/remoteentry.js"
            },
			shared: {
				"react": { "singleton": true },
				"react-dom": { "singleton": true }
			},
		}),
    ]
};

module.exports = merge(commonConfig, prodConfig);