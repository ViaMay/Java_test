var DriverProvider = require('./selenium-server/driver-provider');
var assert = require('assert');
var test = require('selenium-webdriver/testing');

var Page = require('./page-objects/yandex');

test.describe('Yandex home page', function () {

	var driverProvider = new DriverProvider();

	test.before(function () {
		return driverProvider.startUp();
	});

	test.after(function () {
		return driverProvider.tearDown();
	});

	test.it('should be valid', function () {
		var driver = driverProvider.getDriver();
		var page = new Page(driver);
		return page
			.open(page.getReadyCondition())
			.validate();
	});

	test.it('should have at least 9 tabs above the search', function () {
		var driver = driverProvider.getDriver();
		var page = new Page(driver);
		return page
			.open(page.getReadyCondition())
			.getMenuComponent()
			.getItems()
			.then(function (items) {
				/*
				items == [
					{ text: 'Поиск', href: null }
					{ text: 'Карты', href: 'http://maps.yandex.ru/' }
					{ text: 'Маркет', href: 'http://market.yandex.ru/?clid=505' }
					{ text: 'Новости', href: 'http://news.yandex.ru/' }
					{ text: 'Словари', href: 'http://slovari.yandex.ru/' }
					{ text: 'Картинки', href: 'http://yandex.ru/images/' }
					{ text: 'Видео', href: 'http://yandex.ru/video' }
					{ text: 'Музыка', href: 'http://music.yandex.ru/' }
					{ text: 'Перевод', href: 'http://translate.yandex.ru/' }
					{ text: 'ещё', href: 'http://www.yandex.ru/all' }
				]
				 */
				assert.ok(items.length > 9);
			});
	});

	test.it('should have a weather widget', function () {
		var driver = driverProvider.getDriver();
		var page = new Page(driver);
		return page
			.open(page.getReadyCondition())
			.getWeatherComponent()
			.getTemperature()
			.then(function (temperature) {
				/* temperature == '+4 °C' */
				assert.ok(temperature);
			});
	});

});
