import React from "react";
import styled from "styled-components";
import { Link } from 'react-router-dom';
import { screenSize } from "../../constants/screenBreakpoints";
import { colors } from '../../constants/colors';
import logo from '../../static/Icon/GPS_logo.svg';
import H3 from '../ui/H3';
import P from '../ui/P';
import Underline from '../ui/Underline';

import facebookImg from "../../static/Icon/facebook.svg";
import instagramImg from "../../static/Icon/instagram.svg";
import linkedinImg from "../../static/Icon/linkedin.svg";

const StyledContainer = styled.div`
  /* height: 616px; */
  background-color: #373737;
  padding: 200px 10% 100px 10%;
  @media ${screenSize.size960} {
    padding: 100px 5% 80px 5%;
  }
  .container {
    height: 100%; 
    display: flex;
    flex-direction: row;
    justify-content: center;
    align-items: flex-start;
    
    .logo {
        width: 83px;
        margin-right: 15%;
      }
    .flex-item {
      margin: 0 5%;
      .social-icons {
        margin-top: 20px;
        * {
          margin-right: 22px;
        }
      }
      li {
        margin-bottom: 8px;
      }
    }
    @media ${screenSize.size960} {
      flex-direction: column;
      align-items: center;
      .logo {
        margin: 0 0 70px 0;
      }
      .flex-item {
        text-align: center;
        margin: 20px;
        .social-icons {
          display: flex;
          justify-content: center;
          align-items: flex-start;
          * {
            margin: 0 5px;
          }
        }
      }
    }
  }
  img {
    width: 27px;
  }
  ul {
    list-style: none;
    margin-top: 20px;
  }
  .underline {
    margin-top: 100px;
    @media ${screenSize.size960} {
      margin-top: 50px;
    }
  }
  .company-name {
    margin-top: 20px;
  }
`;

const StyledLink = styled(Link)`
  text-decoration: none;
  * {
    &:hover {
      color: ${colors.primaryGreen};
      font-weight: 700;
    }
  }
`;

class Footer extends React.Component {
    constructor() {
        super();
    }

    render() {
        return (
            <StyledContainer>
              <div className='container'>
                <img className='logo' src={logo}></img>
                <div className='flex-item'>
                  <H3 color='#f5f5f5' borderBottom uppercase>company</H3>
                  <ul>
                    <li><StyledLink to='/about'><P color='#f5f5f5'>About us</P></StyledLink></li>
                    <li><StyledLink to='/blog'><P color='#f5f5f5'>Blog</P></StyledLink></li>
                    <li><StyledLink to='/news'><P color='#f5f5f5'>News</P></StyledLink></li>
                    <li><StyledLink to='/career'><P color='#f5f5f5'>Career</P></StyledLink></li>
                    <li><StyledLink to='/contact'><P color='#f5f5f5'>Contact Us</P></StyledLink></li>
                  </ul>
                </div>
                <div className='flex-item'>
                  <H3 color='#f5f5f5' borderBottom uppercase>learn more</H3>
                  <ul>
                    <li><StyledLink to='/pricing'> <P color='#f5f5f5'>Pricing</P></StyledLink></li>
                    <li><StyledLink to='/blog/write-for-us'><P color='#f5f5f5'>Write for us</P></StyledLink></li>
                    <li><StyledLink to='/blog/how-to-join'><P color='#f5f5f5'>How to join</P></StyledLink></li>
                    <li><StyledLink to='/team'><P color='#f5f5f5'>How to join</P></StyledLink></li>
                  </ul>
                </div>
                <div className='flex-item'>
                  <H3 color='#f5f5f5' borderBottom uppercase>connect with us</H3>
                  <div className='social-icons'>
                    <img src={facebookImg}></img>
                    <img src={instagramImg}></img>
                    <img src={linkedinImg}></img>
                  </div>
                </div>
              </div>
              <Underline className='underline' backgroundColor='#919191' width='100%' height='2px'></Underline>
              <P className='company-name' color='#f5f5f5' center>© 2019 GPS IT Solutions</P>
            </StyledContainer>
        );
    }
}

export default Footer;