// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  ServiceUrl:"https://r7g4p2b5bgwqkjp3gf2bwp2wum0ewivx.lambda-url.eu-north-1.on.aws",
  //ServiceUrl:"https://localhost:5001",
  //ServiceUrl:"https://localhost:5001",
  allowedDomains:['https://r7g4p2b5bgwqkjp3gf2bwp2wum0ewivx.lambda-url.eu-north-1.on.aws'], 
  production: false
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
