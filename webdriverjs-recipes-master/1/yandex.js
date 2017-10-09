var until = require('selenium-webdriver').until;
var promise = require('selenium-webdriver').promise;
var By = require('selenium-webdriver').By;

var logoLocator = By.css('.b-logo');

var Page = function (driver) {
	this._driver = driver;
};

Page.prototype.url = 'http://www.yandex.ru/';

Page.prototype.open = function (condition) {
	this._driver.get(this.url);
	this.waitReady(condition);
	return this;
};

Page.prototype.waitReady = function (condition, timeout) {
	if (condition) {
		timeout = timeout || 10 * 1000;
		this._driver.wait(condition, timeout);
	}
	return this;
};

Page.prototype.getReadyCondition = function () {
	return until.elementLocated(logoLocator);
};

Page.prototype.getWeatherComponent = function () {
	var Component = require('./weather-component');
	return new Component(this._driver);
};

Page.prototype.getMenuComponent = function () {
	var Component = require('./menu-component');
	return new Component(this._driver);
};

Page.prototype.validate = function () {
	return promise
		.all([
			this.getMenuComponent().validate(),
			this.getWeatherComponent().validate()
		])
		.then(function (results) {
			return results[0] && results[1];
		});
};

module.exports = Page;
