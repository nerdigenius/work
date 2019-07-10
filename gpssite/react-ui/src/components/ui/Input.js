import React from 'react';
import styled from 'styled-components';

const INPUT = styled.input`
font-family:roboto;
background:transparent;
border:${props => props.border? '1px solid white' : null};
margin:${props => props.margin ? '2px' : null};
border-radius:${props => props.borderRadius ? '2px' : null};
text-align:${props => props.textAlign ? 'center' : null};
display:flex;
      flex-wrap:wrap;
      width:140px;
      height:50px;
      margin: 0;
      
      border:.5px solid white;
      color:white;
      font-size:12px;
      justify-content:center;
      border-radius:4px;
      font-weight:${props => props.fontweight ? 'regular' : null};
      outline:none;
      padding:${props => props.padding ? '2%' : null};

&:hover{
    background-color: ${props => props.hoverBackgroundColor};
    border-color: ${props => props.hoverBorderColor};
    color: ${props => props.hoverColor};
  }
  
`;

export default INPUT;