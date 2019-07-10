import React from 'react';
import styled from 'styled-components';
import { NavLink } from 'react-router-dom';
import { screenSize } from "../../constants/screenBreakpoints";

const Button = styled.div`
  font-size: 1em;
  border: 2px solid;
  border-color: ${props => props.borderColor};
  color: ${props => props.color};
  padding: 1em 2.3em;
  border-radius: 30px;
  box-sizing: border-box;
  font-family: 'Roboto';
  font-weight: 700;
  display: inline-block;
  text-align: center;
  &:hover{
    background-color: ${props => props.hoverBackgroundColor};
    border-color: ${props => props.hoverBorderColor};
    color: ${props => props.hoverColor};
    cursor: pointer;
  }

  @media ${screenSize.size650} {
    font-size: 0.8em;
    min-width:150px;
    width: 40%;
    display: inline-block;
  }
`;

export default Button;