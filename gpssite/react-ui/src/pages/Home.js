import React, { Component } from 'react';
import styled from 'styled-components';
import { createGlobalStyle } from 'styled-components';

import Header from '../components/header/Header';
import Landing from '../components/landing/Landing';
import WhatWeDo from '../components/whatWeDo/WhatWeDo';
import Projects from '../components/projects/Projects';
import Team from '../components/team/Team';
import Client from '../components/clients/Clients';
import Contact from '../components/contact/Contact';
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
