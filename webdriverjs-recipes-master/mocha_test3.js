// "use strict";
// var File = require('file');
//
// var h = new File("C:\chromedriver.exe");
// var wd = require('webdriver-sync');
// var seleniumBinaries = require('selenium-binaries');
// var ChromeDriverService = wd.ChromeDriverService;
// var ChromeDriver = wd.ChromeDriver;
// var service = new ChromeDriverService.Builder()
//     .usingAnyFreePort()
//     .usingDriverExecutable(new File(seleniumBinaries.chromedriver))
//     // .usingDriverExecutable(h)
//     .build();
//
// var driver = new ChromeDriver(service);
// driver.get('http://google.com');


var wd = require("webdriver-sync");
var caps = new wd.DesiredCapabilities["chrome"]();
// var driver = new wd["ChromeDriver"](caps);


// var XMLHttpRequest = require("xmlhttprequest").XMLHttpRequest;
//
// var http = new XMLHttpRequest();
// var url = "http://api.skapis.co/api/top/getTopLooks";
// var params = null;
// http.open("GET", url, false);
// http.send(params);
// console.log(http.responseText);




// var webdriver = require('selenium-webdriver');
//
// var browser = new webdriver.Builder().usingServer().withCapabilities({'browserName': 'chrome' }).build();
// browser.get('http://en.wikipedia.org/wiki/Wiki');
// browser.findElements(webdriver.By.css('[href^="/wiki/"]')).then(function(links){
//     console.log('Found', links.length, 'Wiki links.' )
//     browser.quit();
// });
// // run it once before tests
// test.beforeEach(function() {
//     this.timeout(timeOut);
//     driver = baseTest.SetUp();
// });
//
// test.afterEach(function() {
//     this.timeout(timeOut);
//     baseTest.TearDown();
// });
//
// // tests
// test.describe('My Website', function(){
//     this.timeout(timeOut);
//     test.it('Set field', function() {
//         var defaultPage = new DefaultPage(driver);
//         var signInSignUpPage = new SignInSignUpPage(driver);
//         defaultPage.open()
//             .then(function() {defaultPage.getSignInButton().click(); })
//             .then(function() {signInSignUpPage.open();})
//             //.then(function() {signInSignUpPage.getSignInEmailLink().click();
//             //});
//             });
// });