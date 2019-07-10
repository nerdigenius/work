import React from 'react';
import styled from 'styled-components';

const Underline = styled.div`
  background-color: ${props => props.backgroundColor};
  height: ${props => props.height};
  width: ${props => props.width};
  position: relative;
  left: 50%;
  transform: translate(-50%, 0);
`;

export default Underline;