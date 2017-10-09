var assert = require('assert');
var until = require('selenium-webdriver').until;
var promise = require('selenium-webdriver').promise;
var By = require('selenium-webdriver').By;

var widgetLocator = By.css('.b-weather');
var titleLocator = By.css('.b-content-item__title > .b-link:nth-child(1)');
var temperatureLocator = By.css('.b-content-item__title > .b-link:nth-child(3)');

var Component = function (driver) {
	this._driver = driver;
};

Component.prototype._getWidget = function () {
	var driver = this._driver;
	return driver.isElementPresent(widgetLocator)
		.then(function (result) {
			if (!result) {
				throw new Error('Weather component is not found');
			}
			return driver.findElement(widgetLocator);
		});
};

Component.prototype.validate = function () {
	return this._getWidget()
		.then(function (widget) {
			var title = widget.findElement(titleLocator);
			return title.getText()
				.then(function (text) {
					assert.equal(text, '\u041f\u043e\u0433\u043e\u0434\u0430');
					return true;
				});
		});
};

Component.prototype.getTemperature = function () {
	return this._getWidget()
		.then(function (widget) {
			var temperature = widget.findElement(temperatureLocator);
			return temperature.getText()
				.then(function (text) {
					assert.notEqual(text, '');
					return text;
				});
		});
};


module.exports = Component;
