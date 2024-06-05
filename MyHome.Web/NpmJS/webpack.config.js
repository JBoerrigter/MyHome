const path = require('path');

module.exports = {
    entry: './src/main.js',
    output: {
        filename: 'main.bundle.js',
        path: path.resolve(__dirname, '../wwwroot/js')
    },
    resolve: {
        extensions: ['.js', '.json'],
        modules: [path.resolve(__dirname, 'node_modules'), 'node_modules']
    },
    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /node_modules/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: ['@babel/preset-env']
                    }
                }
            }
        ]
    },
    mode: 'production'
};
