
var SELENIUM_SERVER_JAR = path.resolve(__dirname, 'selenium-server-standalone-3.0.0-beta2.jar');

function DriverProvider() {
	this._server = null;
	this._driver = null;
}

DriverProvider.prototype = {


	startUp: function () {
		var flow = promise.controlFlow();
		return promise.all([
			flow.execute(this.startServer.bind(this)),
			flow.execute(this.buildDriver.bind(this))
		]);
	},

	tearDown: function () {
		var flow = promise.controlFlow();
		return promise.all([
			flow.execute(this.destroyDriver.bind(this)),
			flow.execute(this.stopServer.bind(this))
		]);
	}

};
