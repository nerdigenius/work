import React from 'react';
import styled from 'styled-components';
import { screenSize } from '../../constants/screenBreakpoints';
import H3 from '../ui/H3';
import P from '../ui/P';

import { colors } from '../../constants/colors';

const StyledCard = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  margin: 50px;
  /* width: 200px; */
  
  img {
    width: 150px;
    height: 150px;
    object-fit: cover;
    border: 4px solid white; 
    border-radius: 50%;
    margin-bottom: 20px;
    @media ${screenSize.mobile} {
      width: 100px;
      height: 100px;
    }

  }
  img:hover{
    border: 4px solid ${colors.primaryGreen}; 
  }
  h3 {
    margin-bottom: 5px;
  }
`;

const Card = (props) => {
  return(
    <StyledCard>
      
      <img src={props.person.img}></img>
      <H3 color={colors.lightGrey} center>{props.person.name}</H3>
      <P color={colors.lightGrey} center>{props.person.designation}</P>
    </StyledCard>
  );
}

export default Card;