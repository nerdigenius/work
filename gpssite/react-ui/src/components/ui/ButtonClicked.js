import React from 'react';
import styled from 'styled-components';
import { screenSize } from "../../constants/screenBreakpoints";

const ButtonClicked = styled.div`
  font-size: 1em;
  border: 1px solid;
  border-color: ${props => props.borderColor};
  color: ${props => props.color};
  padding: 1em 0.2em;
  border-radius: 5px;
  box-sizing: border-box;
  font-family: 'Roboto';
  font-weight: 400;
  font-size: 0.9vw;
  text-transform: uppercase;
  ${'' /* display: inline-block; */}
  text-align: center;
  &:hover{
    background-color: ${props => props.hoverBackgroundColor};
    border-color: ${props => props.hoverBorderColor};
    color: ${props => props.hoverColor};
    cursor: pointer;
  }

  @media ${screenSize.size650} {
    font-size: 0.8em;
    width: 40%;
    display: inline-block;
  }
`;

export default ButtonClicked;