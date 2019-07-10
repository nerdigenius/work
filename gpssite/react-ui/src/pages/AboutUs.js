import React, { Component } from 'react';
import styled from 'styled-components';
import { createGlobalStyle } from 'styled-components';

import Header from '../components/header/Header';
import BlogLanding from '../components/landing/BlogsLanding';
import AboutUsPage from '../components/aboutUsPage/AboutUsPage';
import Footer from '../components/footer/Footer';

const GlobalStyle = createGlobalStyle`
	
`;

const StyledContainer = styled.div`
  
`;

class AboutUS extends Component {
  render() {
    return (

      <div>
        <StyledContainer>
          <GlobalStyle />
          
          <Header sticky="false"/>
          <AboutUsPage />
          <Footer />
        
        </StyledContainer>
        {/* <Footer /> */}
      </div>
    );
  }
}

export default AboutUS;
