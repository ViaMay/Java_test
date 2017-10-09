"use strict";

require('chromedriver');

var webdriver = require('selenium-webdriver');
var driver = new webdriver
    .Builder()
    .withCapabilities(
        {
            'browserName': 'chrome'
        })
    .build();

var BaseTests = function BaseTests() {
};

BaseTests.prototype.SetUp = function() {
    driver.manage().deleteAllCookies();
    return driver;
};

BaseTests.prototype.TearDown = function() {
    driver.manage().deleteAllCookies()
        .then(function() {driver.executeScript("localStorage.clear();"); })
        .then(function() {driver.quit();});
};

module.exports = BaseTests;