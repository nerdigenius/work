import React from 'react';
import styled from 'styled-components';
import { screenSize } from "../../constants/screenBreakpoints";

const H1 = styled.h1`
  font-family: 'Poppins';
  font-size: 3em;
  font-weight: 600;
  /* letter-spacing: 0.7px; */
  text-transform: ${props => props.uppercase ? 'uppercase' : null};
  color: ${props => props.color};
  /* padding-bottom: 3px; */
  text-align: ${props => props.center ? 'center' : null};
  border-bottom: ${props => props.borderBottom ? '1px solid #f5f5f5' : null};

  @media ${screenSize.laptopL} {
    font-size: 4em;
  }
  
  @media ${screenSize.size650} {
    font-size: 1.8em;
  }
`;

export default H1;