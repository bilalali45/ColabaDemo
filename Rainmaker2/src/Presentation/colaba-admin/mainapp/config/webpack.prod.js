const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const TerserPlugin = require("terser-webpack-plugin");
const { merge } = require('webpack-merge');
const commonConfig = require('./webpack.common');


const ModuleFederationPlugin = require("webpack").container.ModuleFederationPlugin;

const prodConfig = {
    mode: "production",
    output: {
        publicPath: '/mainapp'
    },
    plugins: [
        new CopyPlugin({
            patterns: [
                { from: "public/envconfig.js", to: "./" },
                { from: "public/web.config", to: "./" },
                // { from: "other", to: "public" },
            ],
        }),
        new ModuleFederationPlugin({
			name: "masterapp",
			remotes: {
                mfloanportal: "loanportalapp@http://172.16.100.117:8073/loanportal/remoteEntry.js"
            },
			shared: {
				"react": { "singleton": true },
				"react-dom": { "singleton": true }
			},
		}),
    ]
};

module.exports = merge(commonConfig, prodConfig);