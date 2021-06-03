const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

const ModuleFederationPlugin = require("webpack").container.ModuleFederationPlugin;

module.exports = {
    entry: "./index.tsx",
    target: "web",
    mode: "development",
    devServer: {
        contentBase: path.join(__dirname, "dist"),
        port: 3001,
        historyApiFallback: true,
        headers: {
            "Access-Control-Allow-Origin": "*",
            "Access-Control-Allow-Methods": "GET, POST, PUT, DELETE, PATCH, OPTIONS",
            "Access-Control-Allow-Headers": "X-Requested-With, content-type, Authorization"
        },
        hot: false,
        inline: false,
        liveReload: false,
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
                loader: "css-loader"

			},
			{
				test: /\.(scss|css)$/,
				use: ['style-loader', 'css-loader', 'sass-loader'],
			},
            {
                test: /\.(png|svg|jpg|jpeg|gif)$/i,
                type: 'asset',
            },

            {
                test: /\.(woff|woff2|eot|ttf|otf)$/i,
                type: 'asset/resource',
            },
		],
	},
	plugins: [
		new ModuleFederationPlugin({
			name: "masterapp",
			remotes: {
                mfloanportal: "loanportalapp@http://localhost:3002/remoteentry.js"
            },
			shared: {
				"react": { "singleton": true },
				"react-dom": { "singleton": true }
			},
		}),
		new HtmlWebpackPlugin({
			template: './public/index.html',
		}),
		new MiniCssExtractPlugin({
			filename: "static/css/styles.css",
		}),
	]
};