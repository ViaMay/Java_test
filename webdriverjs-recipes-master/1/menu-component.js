var assert = require('assert');
var until = require('selenium-webdriver').until;
var promise = require('selenium-webdriver').promise;
var By = require('selenium-webdriver').By;

var tabsLocator = By.css('.b-head-tabs');
var tabItemLocator = By.css('.b-head-tabs__tab');
var selectedTabItemLocator = By.css('.b-head-tabs__tab_selected');
var linkLocator = By.css('.b-head-tabs__tab-link');

var Component = function (driver) {
	this._driver = driver;
};

Component.prototype._getTabs = function () {
	var driver = this._driver;
	return driver.isElementPresent(tabsLocator)
		.then(function (result) {
			if (!result) {
				throw new Error('Menu component is not found');
			}
			return driver.findElement(tabsLocator);
		});
};

Component.prototype.validate = function () {
	return this._getTabs()
		.then(function (widget) {
			assert.ok(widget);
			return this.getItems();
		}.bind(this))
		.then(function (items) {
			assert.ok(items);
			assert.ok(items.length > 0);
		});
};

Component.prototype.getItems = function () {

	function itemMapper(item) {
		return item.isElementPresent(linkLocator)
			.then(function (result) {
				var link, out;
				if (result) {
					link = item.findElement(linkLocator);
					out = promise.all([
						link.getText(),
						link.getAttribute('href')
					]);
				} else {
					out = promise.all([
						item.getText(),
						null
					]);
				}
				return out;
			})
			.then(function (hash) {
				return hash && {
						text: hash[0],
						href: hash[1]
					};
			});
	}

	return this._getTabs()
		.then(function (widget) {
			return widget.findElements(tabItemLocator);
		})
		.then(function (items) {
			return promise.map(items, itemMapper);
		});
};

module.exports = Component;
