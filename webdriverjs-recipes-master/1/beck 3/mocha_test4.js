var assert = require('assert');
var selenium = require('selenium-webdriver');

describe('calculating weights', function() {
    it('calculates weights', function() {
        var driver = new selenium.Builder()
            .withCapabilities(
                {
                    'browserName': 'chrome'
                })
            .build();

        driver.get("https://decohere.herokuapp.com/planets");

        var weight = driver.findElement(selenium.By.id('wt')).isDisplayed();
        assert.equal(weight, true, "Weight entry not possible");

        driver.quit();
    });
});