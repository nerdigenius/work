import React from 'react';
// import ReactDOM from 'react-dom';
import { hydrate, render } from "react-dom";
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import { Router } from 'react-router-dom';
import history from "./history";

// ReactDOM.render(
//   <Router history={history}>
//     <App />
//   </Router>, document.getElementById('root'));
// registerServiceWorker();

const rootElement = document.getElementById('root');
if (rootElement.hasChildNodes()) {
    hydrate(<Router history={history}>
      <App />
    </Router>, rootElement);
    ServiceWorker.register();
} else {
    render(<Router history={history}>
      <App />
    </Router>, rootElement);
}
registerServiceWorker();
