import React, { Component } from 'react';
import styled from 'styled-components';
import { createGlobalStyle } from 'styled-components';

import Header from '../components/header/Header';
import Landing from '../components/landing/landing';
import WhatWeDo from '../components/whatWeDo/whatWeDo';
import Projects from '../components/projects/projects';
import Team from '../components/team/Team';
import Client from '../components/clients/clients';
import Contact from '../components/contact/contact';
import Footer from '../components/footer/Footer';
// import WhatWeDo from '../components/whatWeDo/whatWeDo';

const GlobalStyle = createGlobalStyle`
	
`;

const StyledContainer = styled.div`
  
`;

class Home extends Component {
  render() {
    return (

      <div>
        <StyledContainer>
          <GlobalStyle />
          
          <Header />
          <Landing />
          <WhatWeDo />
          <Projects />
          <Team />
          <Client />
          <Contact />
          <Footer />
        
        </StyledContainer>
        {/* <Footer /> */}
      </div>
    );
  }
}

export default Home;
