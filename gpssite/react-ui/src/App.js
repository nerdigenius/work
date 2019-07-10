import React, { Component } from 'react';
import { createGlobalStyle } from 'styled-components';

import { Route } from "react-router-dom";
import Home from './pages/Home';
import Service from './pages/Service';
import Projects from './pages/Projects';
import Blogs from './pages/Blogs';
import AboutUs from './pages/AboutUs';


import Pricing from './pages/Pricing';

const GlobalStyle = createGlobalStyle`
${'' /* @import url('https://fonts.googleapis.com/css?family=Roboto:100,400,700&display=swap');
@import url('https://fonts.googleapis.com/css?family=Montserrat:400,700,800,900&display=swap'); */}
	*,
	*::after,
	*::before {
		margin: 0;
		padding: 0;
    	box-sizing: border-box;
	}
	html{
		color: black;
		margin: 0;
		padding: 0;
    	height: 100%;
	}
	body {
		margin: 0;
		padding: 0;
		height: 100%;
	}

	h1,h2,h3,h4,h5,h6,p{
		margin: 0;
		padding: 0;
	}
`;

class App extends Component {
	render() {
		return (
			<div>
				<GlobalStyle />
				<Route
					exact
					path='/'
					render={props => <Home {...props} />}
				/>
				<Route
					exact
					path='/services'
					render={props => <Service {...props} />}
				/>
				<Route
					exact
					path='/projects'
					render={props => <Projects {...props} />}
				/>
		        <Route
					exact
					path='/blogs'
					render={props => <Blogs {...props} />}
				/>
				<Route
					exact
					path='/abouus'
					render={props => <AboutUs {...props} />}
				/>
		
		     	
		       <Route
					exact
					path='/pricing'
					render={props => <Pricing {...props} />}
				/>
				
			</div>
		);
	}
}

export default App;
