for install galen use command:
npm install galenframework-cli

for start test use command:
galen test tests/tests/login1.test.js

for start test with report use command:
galen test tests/tests/login.test.js --htmlreport reports
google-chrome reports/report.html

for start all tests used:
galen test tests/tests/

for idea plugin: https://plugins.jetbrains.com/plugin/8302?pr=phpStorm&showAllUpdates=true

for BrowserStack: https://github.com/browserstack/Galen-BrowserStack
USERNAME = "andreiarnautov1";
AUTOMATE_KEY = "yiFysr9R61GJTRg1TDzt";
galen test tests/tests/ -Dbrowserstack.username=<USERNANME> -Dbrowserstack.key=<KEY>
galen test tests/tests/login2.test.js -Dbrowserstack.username=andreiarnautov1 -Dbrowserstack.key=yiFysr9R61GJTRg1TDzt

parallel:  --parallel-suites 2


help:
https://github.com/galenframework/galen-sample-tests/blob/master/tests/pages/WelcomePage.js

