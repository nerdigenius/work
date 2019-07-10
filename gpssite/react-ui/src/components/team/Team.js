import React from "react";
import styled from "styled-components";
import { screenSize } from "../../constants/screenBreakpoints";
import { colors } from '../../constants/colors';
import { team } from '../../constants/team';

import H3 from '../ui/H3';
import Underline from '../ui/Underline';
import Card from './DevCard';

const StyledContainer = styled.div`
  /* height: 100vh; */
  padding: 60px 220px;
  background-color: ${colors.darkGrey};
  color: ${colors.lightGrey};
  @media ${screenSize.size1366} {
    padding: 60px 220px;
  }
  @media ${screenSize.size1200} {
    padding: 60px 180px;
  }
  @media ${screenSize.size960} {
    padding: 60px 100px;
  }
  @media ${screenSize.mobile} {
    padding: 60px 40px;
  }
  
  .title {
    margin-bottom: 20px;
  }
  .row {
    /* display: flex; */
    width: 100%;
    overflow: hidden;
    border-radius: 30%;
    .slider {
      display: flex;
      animation: 15s slideLeft infinite;
      @media ${screenSize.mobileS} {
        animation: 15s slideLeftSmallScreen infinite;
      }
    }
  }
  @keyframes slideLeft {
    0% {
      transform: translate(0);
    }
    16.666% {
      transform: translate(-250px);
    }
    33.333% {
      transform: translate(-500px);
    }
    50% {
      transform: translate(-750px);
    }
    66.666% {
      transform: translate(-1000px);
    }
    83.333% {
      transform: translate(-1250px);
    }
    100% {
      transform: translate(-1500px);
    }
  }
  @keyframes slideLeftSmallScreen {
    0% {
      transform: translate(0);
    }
    16.666% {
      transform: translate(-200px);
    }
    33.333% {
      transform: translate(-400px);
    }
    50% {
      transform: translate(-600px);
    }
    66.666% {
      transform: translate(-800px);
    }
    83.333% {
      transform: translate(-1000px);
    }
    100% {
      transform: translate(-1200px);
    }
  }
`;


class Team extends React.Component {
    constructor() {
      super();
    }

    render() {
      return (
        <StyledContainer>
          <H3 className='title' color="white"center uppercase>Meet Our Team</H3>
          <Underline backgroundColor="white" height='2px' width='56px'></Underline>
          <div className='row'>
            <div className='slider'>
              {
                team.map(member => {
                  return <Card person={member}></Card>
                })
                
              }
              <Card person={team[0]}></Card>
              <Card person={team[1]}></Card>
              <Card person={team[2]}></Card>
              <Card person={team[3]}></Card>
              <Card person={team[4]}></Card>
              {/* <Card person={team[5]}></Card>
              <Card person={team[0]}></Card>
              <Card person={team[1]}></Card>
              <Card person={team[2]}></Card> */}
            </div>
          </div>
        </StyledContainer>
      );
    }
}

export default Team;