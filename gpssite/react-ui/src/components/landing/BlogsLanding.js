import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import "react-responsive-carousel/lib/styles/carousel.min.css";
import { Carousel } from 'react-responsive-carousel';

import styled from "styled-components";
import { screenSize } from "../../constants/screenBreakpoints";
import H1 from "../ui/H1";
import Button from "../ui/Button";
import RightUnderline from '../ui/RightUnderline';

import landingImg from "../../static/Icon/Mask-Group-32.png";
import facebookImg from "../../static/Icon/facebook.svg";
import instagramImg from "../../static/Icon/instagram.svg";
import linkedinImg from "../../static/Icon/linkedin.svg";

const StyledContainer = styled.div`
    .container{
        max-height: 100vh;
        position: relative;
    }

    .carousel.carousel-slider{
        max-height: 100vh;
    }

    .carousel .slide img{
        filter: brightness(30%);
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
        top: 18%;
        left: 0;
        /* transform: translate(0, -50%); */
        width: 100%;
        z-index: 1;
        color: #fff;
        text-align: left;
        align-items: center;
        display: flex;
    }

    .col-8{
        width: 60%;
        padding: 4% 0 0 8%;
    }

    .col-4{
        width: 40%;
        padding: 4% 0 0 8%;
    }

    .header{
        margin-bottom: 40px;
    }

    .sub-title{
        font-family: 'Roboto';
        margin-bottom: 20px;
        font-weight: 100;
    }

    .technology{
        margin-bottom: 60px;
        font-size: 14px;
    }

    .technology-title{
        font-size: 1.5em;
        font-weight: 500;
        font-family: 'Poppins';
        color: #ffffff9e;
    }

    .underline{
        margin-bottom: 5px;
    }

    .technology .sub-title{
        margin-bottom: 5px;
        color: #ffffff9e;
    }

    .desc{
        font-size: 1.1em;
        font-family: 'Roboto';
        margin-bottom: 60px;
        font-weight: 300;
    }

    .buttonSection{
        display: flex;
    }

    .buttonArea{
        margin-right: 30px;
    }

    @media ${screenSize.laptopL} {

      .desc{
        font-size: 1.3em;
      }

      .technology{
            font-size: 16px;
      }
    }

    @media ${screenSize.size650} {

        .header{
            margin-bottom: 20px;
        }

        .desc{
            font-size: 0.8em;
            margin-bottom: 25px;
        }

        .buttonSection{
            flex-direction: column;
        }

        .buttonArea{
            margin-bottom: 20px;
            margin-right: 0;
        }

        .buttonArea div{
            width: 50%;
        }

        .header-container{
            top: 35%;
            // text-align: center;
            width: 100%;
        }

        .col-8{
            width: 100%;
            padding: 0 8%;
        }

        .col-4{
            display: none;
        }
    }
    
    @media ${screenSize.size960} and (min-width: 650px) {
        .header-container{
            top: 50%;
            transform: translate(0, -50%);
        }
        .col-8{
            margin-top: 20%;
        }
        .sub-title{
        margin-top:7%;
      }
    }

    @media ${screenSize.size1200} {
        
    }
    @media screen and (max-width:400px){
      .sub-title{
        margin-top:7%;
      }
    }
`;

class BlogLanding extends React.Component {
    constructor() {
        super();
    }

    render() {
        return (
            <StyledContainer>
                <div className="container">

                    <Carousel showIndicators={false} showArrows={false} showThumbs={false} showStatus={false}>
                        <div>
                            <div className="header-container">
                                <div className="col-8">
                                    <div className="header">
                                        <div className="sub-title">
                                            WEB TECHOLOGY, LANGUAGE
                                        </div>
                                        <div className="title">
                                            <H1>How To Choose The Best Web Development Company?</H1>
                                        </div>
                                    </div>
                                    <div className="body">
                                        <div className="desc">
                                            Hello again! It’s the third time I get to share with you some tips and
                                        tricks on time <br />
                                            management and I am certain that by now
                                            you’ve had enough time to introduce some

                                    </div>
                                        <div className="buttonSection">
                                            <div className="buttonArea">
                                                {/* <div className="button">
                                                SEE OUR PORTFOLIO
                                            </div> */}
                                                <Button hoverBackgroundColor="#01c476" hoverBorderColor="#01c476">READ MORE</Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div className="col-4">
                                    <div className="technologies">
                                        <div className="technology">
                                            <div className="sub-title">WEB TECHOLOGY, LANGUAGE</div>
                                            <div className="underline">
                                                <RightUnderline backgroundColor="#ffffff9e" width="100px" height="2px"></RightUnderline>
                                            </div>
                                            <div className="technology-title">
                                                5 tips to great-<br />website
                                            </div>
                                        </div>
                                    </div>
                                    <div className="technologies">
                                        <div className="technology">
                                            <div className="sub-title">C#, PYTHON</div>
                                            <div className="underline">
                                                <RightUnderline backgroundColor="#ffffff9e" width="100px" height="2px"></RightUnderline>
                                            </div>
                                            <div className="technology-title">
                                                C# vs Python : watch <br />the comparision
                                            </div>
                                        </div>
                                    </div>
                                    <div className="technologies">
                                        <div className="technology">
                                            <div className="sub-title">DIGITAL MARKETING, MOBILE</div>
                                            <div className="underline">
                                                <RightUnderline backgroundColor="#ffffff9e" width="100px" height="2px"></RightUnderline>
                                            </div>
                                            <div className="technology-title">
                                                5 tips to digital-<br />marketing
                                            </div>
                                        </div>
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

export default BlogLanding;