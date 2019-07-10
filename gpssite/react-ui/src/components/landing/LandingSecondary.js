import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import "react-responsive-carousel/lib/styles/carousel.min.css";
import { Carousel } from 'react-responsive-carousel';

import styled from "styled-components";
import { screenSize } from "../../constants/screenBreakpoints";
import H1 from "../ui/H1";
import Button from "../ui/Button";

import bg from "../../static/Icon/bg1.jpg";
import facebookImg from "../../static/Icon/facebook.svg";
import instagramImg from "../../static/Icon/instagram.svg";
import linkedinImg from "../../static/Icon/linkedin.svg";

const StyledContainer = styled.div`
    .container{
        /* max-height: 100vh; */
        background: linear-gradient(rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5)), url(${bg});
        height: auto;
        background-repeat: no-repeat;
        background-size: cover;
        background-position: center center;
    }

    .wrapper{
        padding: 10% 10%;
    }

    .header-container{
        text-align: center;
        color: #fff;
    }

    .header{
        margin-bottom: 40px;
    }

    .desc{
        font-size: 1.1em;
        font-family: 'Roboto';
        margin-bottom: 60px;
    }

    .buttonSection{
        display: flex;
        justify-content: center;
    }

    .buttonArea{
        margin-right: 30px;
    }

    .social-icons{
        position: absolute;
        z-index: 1;
        top: 32%;
        right: 5%; 
        width: 2%;
    }

    .icon{
        margin-bottom: 20px;
    }

    @media ${screenSize.laptopL} {

      .desc{
        font-size: 1.3em;
      }
    }

    @media ${screenSize.size650} {
        .header-container{
            /* margin-top: 5%; */
        }

        .header{
            margin-bottom: 20px;
        }

        .desc{
            font-size: 0.8em;
        }

        .buttonSection{
            flex-direction: column;
        }

        .buttonArea{
            margin-bottom: 20px;
            margin-right: 0;
        }

        .carousel .control-dots{
            bottom: 0;   
        }

        .social-icons{
            display: none;
        }

        .header-container{
            top: 60%;
        }
    }
    
    @media ${screenSize.size960} {
        .social-icons{
            display: none;
        }
    }

    @media ${screenSize.size1200} {
      .social-icons{
          display: none;
      }
  }
`;

class LandingSecondary extends React.Component {
    constructor() {
        super();
    }

    render() {
        return (
            <StyledContainer>
                <div className="container">
                    <div className="wrapper">
                        <div className="header-container">
                            <div className="header">
                                <div className="title">
                                    <H1>GET YOUR SERVICE DONE</H1>
                                </div>
                            </div>
                            <div className="body">
                                <div className="desc">
                                    <p>
                                        We offer wide range of services from UI Design to Software development.
                                        Check out the list of services we listed <br /> below and contact us and
                                        we are happy to work with you.
                                    </p>
                                </div>
                                <div className="buttonSection">
                                    <div className="buttonArea">
                                        <Button hoverBackgroundColor="#01c476" hoverBorderColor="#01c476">SEE OUR PORTFOLIO</Button>
                                    </div>

                                    <div className="buttonArea">
                                        <Button hoverBackgroundColor="#01c476" hoverBorderColor="#01c476">GET OUR PRICE</Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </StyledContainer>
        );
    }
}

export default LandingSecondary;