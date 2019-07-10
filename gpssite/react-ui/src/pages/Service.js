import React, { Component } from 'react';
import styled from 'styled-components';
import { createGlobalStyle } from 'styled-components';

import Header from '../components/header/Header';
import ServiceLanding from '../components/landing/ServiceLanding';
import Feature from '../components/features/Features';
import LandingSecondary from '../components/landing/LandingSecondary';
import Footer from '../components/footer/Footer';
import WeLoveWhatWeDo from '../components/weLoveWhatWe/WeLoveWhatWeDo'

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
          <ServiceLanding />
          <WeLoveWhatWeDo/> 
          <Feature />
          <LandingSecondary />
          <Footer />
        
        </StyledContainer>
        {/* <Footer /> */}
      </div>
    );
  }
}

export default Home;
