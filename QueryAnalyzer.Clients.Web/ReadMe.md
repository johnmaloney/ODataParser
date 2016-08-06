This project is based on the Angular 2 tutorial
https://angular.io/docs/ts/latest/quickstart.html

Start the Node server to observe the site:

npm start - runs the compiler and a server at the same time, both in "watch mode"

npm run tsc - runs the TypeScript compiler once

npm run tsc:w - runs the TypeScript compiler in watch mode; the process keeps running, awaiting changes to TypeScript files and recompiling when it sees them

npm run lite - runs the lite-server, a light-weight, static file server with excellent support for Angular apps that use routing

npm run typings - runs the typings tool separately

npm run postinstall - called by npm automatically after it successfully completes package installation. This script installs the TypeScript definition files defined in typings.json