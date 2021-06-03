const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

const ModuleFederationPlugin = require("webpack").container.ModuleFederationPlugin;

module.exports = {
    entry: "./src/components/index.tsx",
    target: "web",
    mode: "development",
    devServer: {
        contentBase: path.join(__dirname, "dist"),
        port: 3001,
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
                test: /\.(ts|tsx)$/,
                loader: "awesome-typescript-loader",
            },
            {
                enforce: "pre",
                test: /\.js$/,
                loader: "source-map-loader",
            },
            {
                test: /\.(scss|css)$/,
                use: ['style-loader', 'css-loader', 'sass-loader'],
            },
            {
                test: /\.(png|svg|jpg|jpeg|gif)$/i,
                type: 'asset',
            },
        ],
    },
    plugins: [
        new ModuleFederationPlugin({
            name: "masterapp",
            remotes: {
                mfmcu: "mcuapp@http://localhost:3002/remoteEntry.js",
                mfsettings:"settingsapp@http://localhost:3004/remoteEntry.js",
                mfnotifications:"notificationapp@http://localhost:3005/remoteEntry.js",
                mfdocmanager:"docmanagerapp@http://localhost:3006/remoteEntry.js"
            },
            shared: {
                "react":     {"singleton": true}, 
                "react-dom": {"singleton": true}
            },
          }),
        new HtmlWebpackPlugin({
            template: path.resolve(__dirname, "src", "components", "index.html"),
        }),
        new MiniCssExtractPlugin({
            filename: "./src/bundle.css",
        }),
    ]
};