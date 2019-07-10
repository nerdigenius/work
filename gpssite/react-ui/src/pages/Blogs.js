import React, { Component } from 'react';
import styled from 'styled-components';
import { createGlobalStyle } from 'styled-components';

import Header from '../components/header/Header';
import BlogLanding from '../components/landing/BlogsLanding';
import BlogBody from '../components/blogBody/BlogBody';
import Footer from '../components/footer/Footer';

const GlobalStyle = createGlobalStyle`
	
`;

const StyledContainer = styled.div`
  
`;

class Blogs extends Component {
  render() {
    return (

      <div>
        <StyledContainer>
          <GlobalStyle />
          
          <Header />
          <BlogLanding />
          <BlogBody />
          <Footer />
        
        </StyledContainer>
        {/* <Footer /> */}
      </div>
    );
  }
}

export default Blogs;
