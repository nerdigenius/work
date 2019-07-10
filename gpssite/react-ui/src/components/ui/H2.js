import React from 'react';
import styled from 'styled-components';

const H3 = styled.h3`
  font-family: 'Poppins', sans-serif;
  font-size: 1.9em;
  font-weight: 600;
  letter-spacing: 0.7px;
  text-transform: ${props => props.uppercase ? 'uppercase': null};
  color: ${props => props.color};
  line-height: ${props => props.lineHeight};
  padding-bottom: 3px;
  text-align: ${props => props.center ? 'center' : null};
  border-bottom: ${props => props.borderBottom ? '1px solid #f5f5f5' : null};
`;

export default H3;