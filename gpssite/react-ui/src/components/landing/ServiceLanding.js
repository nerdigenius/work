import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import "react-responsive-carousel/lib/styles/carousel.min.css";
import { Carousel } from 'react-responsive-carousel';

import styled from "styled-components";
import { screenSize } from "../../constants/screenBreakpoints";
import H1 from "../ui/H1";
import Button from "../ui/Button";

import landingImg from "../../static/Icon/service-landing.jpg";
import facebookImg from "../../static/Icon/facebook.svg";
import instagramImg from "../../static/Icon/instagram.svg";
import linkedinImg from "../../static/Icon/linkedin.svg";


// style start here
const StyledContainer = styled.div`
    .container{
        max-height: 100vh;
        position: relative;
    }

    .carousel.carousel-slider{
        max-height: 100vh;
    }

    .carousel .slide img{
        filter: brightness(50%);
        object-fit: cover;
        height: 100vh;
    }

    .carousel .control-dots{
        bottom: 15%;   
    }

    .carousel .control-dots .dot.selected, .carousel .control-dots .dot:hover{
        background-color: #01c476;
    }

    .carousel .control-dots .dot{
        width: 12px;
        height: 12px;
    }

    .header-container{
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        width: 80%;
        z-index: 1;
        color: #fff;
        /* padding: 0 10%; */
    }

    .sub-title{
        font-family: 'Roboto';
        margin-bottom: 20px;
        font-weight: 100;
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

    /* .button{
        padding: 1rem 2rem;
        border: 2px solid #fff;
        border-radius: 50px;
        box-sizing: border-box;
        font-family: 'Roboto';
        font-weight: 700;
        font-size: 1em;
    } */

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

        /* .button{
            font-size: 0.7em;
            padding: 1em 1em;
        } */

        .buttonArea{
            margin-bottom: 20px;
            margin-right: 0;
        }

        /* .button{
            width: 50%;
            display: inline-block;
        } */

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

class ServiceLanding extends React.Component {
    constructor() {
        super();
    }

    render() {
        return (
            <StyledContainer>
                <div className="container">
                    <div className="social-icons">
                        <div className="icon">
                            <img src={facebookImg} />
                        </div>
                        <div className="icon">
                            <img src={instagramImg} />
                        </div>
                        <div className="icon">
                            <img src={linkedinImg} />
                        </div>
                    </div>
                    <Carousel showIndicators={false} showArrows={false} showThumbs={false} showStatus={false}>
                        <div>
                            <div className="header-container">
                                <div className="header">
                                    <div className="sub-title">
                                        The stuff we provide
                                    </div>
                                    <div className="title">
                                        <H1>OUR SERVICES</H1>
                                    </div>
                                </div>
                                <div className="body">
                                    <div className="desc">
                                        <p>
                                            We offer wide range of services from UI Design to
                                            Software development. Check out the list
                                            of <br />services we listed below and contact us and we are happy to work with you.
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <img src={landingImg} />
                        </div>
                        <div>
                            <div className="header-container">
                                <div className="header">
                                    <div className="title">
                                        <h1>DEVELOP OUTSTANDING PRODUCTS</h1>
                                    </div>
                                </div>
                                <div className="body">
                                    <div className="desc">
                                        GPS create products that let do people thins differently. <br />
                                        Whats your idea ?
                                    </div>
                                </div>
                            </div>
                            <img src={landingImg} />
                        </div>
                        <div>
                            <div className="header-container">
                                <div className="header">
                                    <div className="title">
                                        <h1>DEVELOP OUTSTANDING PRODUCTS</h1>
                                    </div>
                                </div>
                                <div className="body">
                                    <div className="desc">
                                        <p>
                                            We offer wide range of services from UI Design to
                                            Software development. Check out the list
                                            of services we listed below and contact us and we are happy to work with you.
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <img src={landingImg} />
                        </div>
                    </Carousel>
                </div>
            </StyledContainer>
        );
    }
}

export default ServiceLanding;