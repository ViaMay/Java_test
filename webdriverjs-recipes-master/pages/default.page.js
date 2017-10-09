"use strict";

var webdriver = require('selenium-webdriver');
var SignInButton = webdriver.By.css('div.index_topLoginContainer.col-xs-12 > div > div > button');

var DefaultPage = function DefaultPage(driver) {
    this.driver = driver;
    this.url = "http://stylu.co";
};

DefaultPage.prototype.open = function() {
    this.driver.get(this.url);
    return webdriver.promise.fulfilled(true);
};

DefaultPage.prototype.getSignInButton = function () {
    return this.driver.findElement(SignInButton);
};

module.exports = DefaultPage;