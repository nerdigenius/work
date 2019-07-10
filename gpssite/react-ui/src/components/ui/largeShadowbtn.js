import React from 'react';
import styled from 'styled-components';
import { colors } from '../../constants/colors';


const Shadowbtn = styled.div`
  display:flex;
  flex-direction:column;
  background:none;
  min-width:130px;
  width: ${props => props.width };
  height: ${props => props.height };
  border: ${props => props.Border};
  font-family: ${props => props.Font};
  color: ${props => props.Color};
  border-radius: 6px;
  min-height:120px;
  box-shadow: 0 3px 6px rgba(0, 0, 0, 0.16);
  justify-content: space-around;
  text-align:center;
  align-items: center;
  :hover{
    border:2px solid ${colors.primaryGreen};}


  
  
`;

export default Shadowbtn;