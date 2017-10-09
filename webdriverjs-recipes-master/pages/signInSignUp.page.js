"use strict";

var webdriver = require('selenium-webdriver');
var SignInEmailLink = webdriver.By.xpath('//*[@id-unique="SignInWithEmail"]');
//var SignInEmailLink = webdriver.By.id('registrationModal');
//var SignInEmailLink = webdriver.By.css('div > div > div > button');

var SignInSignUpPage = function SignInSignUpPage(driver) {
    this.driver = driver;
};

SignInSignUpPage.prototype.open = function() {
    var driver = this.driver;
    driver.wait(driver.findElement(SignInEmailLink), 20000)
        .then(function() {
            return webdriver.promise.fulfilled(true);
        });
};

SignInSignUpPage.prototype.getSignInEmailLink = function () {
    var driver = this.driver;
    driver.wait(driver.findElement(SignInEmailLink), 20000)
        .then(function() {
            return driver.findElement(SignInEmailLink);
        });
};

module.exports = SignInSignUpPage;