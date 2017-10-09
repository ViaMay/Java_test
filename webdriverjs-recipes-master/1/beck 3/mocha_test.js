var assert = require('assert'),
    test = require('selenium-webdriver/testing'),
    webdriver = require('selenium-webdriver');
var FormPage = require('./../beck 2/form.page.js');

test.describe('Google Search', function() {

    this.timeout(15000);

    test.before(function () {
        browser = new webdriver.Builder().usingServer().withCapabilities({'browserName': 'chrome' }).build();
    });

    test.after(function () {
        browser.quit();
    });

    test.it('should work', function() {
        browser.get('http://en.wikipedia.org/wiki/Wiki');
        FormPage.open();
    });
});