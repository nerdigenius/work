import React from 'react';
import styled from 'styled-components';

const P = styled.p`
  font-family: 'Roboto', sans-serif;
  font-size: 12px;
  font-weight: 300;
  letter-spacing: 0.6px;
  text-align: ${props => props.center ? 'center' : null};
  color: ${props => props.color};
  border-bottom: ${props => props.borderBottom ? '1px solid #f5f5f5' : null};
`;

export default P;