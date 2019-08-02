import React, { Component } from 'react';
import styled from 'styled-components';
import { createGlobalStyle } from 'styled-components';

import Header from '../components/header/Header';
import Landing from '../components/landing/landing';
import ProjectsGrid from '../components/projectsGrid/ProjectsGrid'

import Footer from '../components/footer/Footer';
// import WhatWeDo from '../components/whatWeDo/whatWeDo';

const GlobalStyle = createGlobalStyle`
	
`;

const StyledContainer = styled.div`
   
  
`;

class Projects extends Component {
  render() {
    return (

      <div>
        <StyledContainer>
          <GlobalStyle />
          
          <Header sticky="false"/>
          <ProjectsGrid/>
          
          <Footer />
        
        </StyledContainer>
        {/* <Footer /> */}
      </div>
    );
  }
}

export default Projects;
