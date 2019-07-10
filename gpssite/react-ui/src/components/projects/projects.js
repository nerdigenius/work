import React from "react";
import styled from "styled-components";
import { screenSize } from "../../constants/screenBreakpoints";
import { colors } from "../../constants/colors";
import ReactDOM from "react-dom";
import "react-responsive-carousel/lib/styles/carousel.min.css";
import { Carousel } from "react-responsive-carousel";
import H1 from "../ui/H1";
import H2 from "../ui/H2";
import H3 from "../ui/H3";
import Underline from "../ui/Underline";
import RightUnderline from "../ui/RightUnderline";
import Button from "../ui/Button";
import sliderImage from "../../static/Icon/Project_image.png";

const StyledContainer = styled.div`
  .carousel.carousel-slider {
    overflow: visible !important;
  }
  .buttoncontainer{
    display:flex;
    flex-grow:1;
  }

  .projectSlide {
    display: flex;
    overflow: hidden;
    height: 60vh;
  }
  .firstHalfSlide {
    flex-basis: 52%;
  }

  .container {
    margin-bottom: 80px;
  }
  .secondHalfSlide {
    flex-basis: 48%;
    background-color: white;
    padding-top: 4%;
    text-align: right;
    padding: 3% 8%;

    .titlePara {
      text-align: right;
      margin-bottom: 10px;
    }

    .underlinePosition {
      ${"" /* left: 71%; */}
      margin-bottom: 10px;
    }
  }

  .projectName {
    color: #373737;
    font-family: "Roboto", sans-serif;
    font-size: 25px;
    font-weight: 700;
    margin-bottom: 8%;
    font-weight: bold;
  }

  .paraProjectText {
    color: #373737;
    font-family: "Montserrat", sans-serif;
    font-size: 16px;
    font-weight: 400;
    width: 100%;
    margin-bottom: 5%;
    line-height: 150%;
  }

  .projectSlideDiv {
    ${"" /* margin: 3% 3%; */}
    margin: 3% 3% 5% 3%;
    .title {
      margin-bottom: 20px;
    }
    .secondaryTitle {
      margin-top: 30px;
      margin-bottom: 30px;
    }
  }

  .titleIntroProjectSlide {
    color: #373737;
    font-family: "Montserrat", sans-serif;
    font-size: 45px;
    font-weight: 700;
    text-transform: uppercase;
    margin: 1% 1%;
    text-align: center;
    margin-top: 34px;
  }

  .paraProjectSlide {
    color: #373737;
    font-family: "Segoe UI";
    font-size: 20px;
    font-weight: 400;
    margin-left: 2%;
    margin-right: 2%;
    padding: 0 1% 0 1%;
    text-align: center;
  }

  .carousel.carousel-slider {
    ${"" /* height: 72vh; */}
  }

  .control-dots {
    ${"" /* border: 1px solid #01c476;
    background-color: #01c476; */}
    top: -54px;
    
  }
  .dot{
    border:1px solid #01c476;
    
  }

  .carousel .control-dots .dot.selected,
  .carousel .control-dots .dot:hover {
    background-color: #01c476;
    border: 1px solid #01c476;
    outline:none
  }

  .carousel .control-dots {
    bottom: initial !important;
    margin:2% 0;
    
  }
  

  .carousel .control-dots .dot {
    width: 12px;
    height: 12px;
    margin: -15px 8px 18px 8px;
    box-shadow:none;
  }

  .carousel .slide {
    background: #fff;
  }

  ${"" /* .readMoreBtn {
    border: 1px solid;
    border-radius: 5px;
  } */}

  ${'' /* .readmoreBtn {
    color: #01c476;
    font-family: Roboto;
    font-size: 13px;
    font-weight: 700;
    text-transform: uppercase;
    border-radius: 50px;
    border: 2px solid;
    padding: 2% 3%;
    width: 45%;
    text-align: center;
    display: inline-block;
  } */}

  .readmoreBtnPosition {
    text-align: right;
  }

  @media ${screenSize.size960} {
    ${"" /* .headerProjectSlide {
      padding: 1% 2%;
      text-align: center;
    } */}

    .projectSlide {
      height: auto;
    }

    .titleIntroProjectSlide {
      padding: 9px 1px;
      font-size: 1.8em;
      margin: 1px 8px;
      text-align: center;
    }

    .paraProjectSlide {
      margin: 1% 2%;
    }
    .paraProjectSlide {
      padding: 1% 1%;
    }

    .secondHalfSlide {
      padding: 0 4% 0px 4%;
    }

    .carousel .control-dots {
      top: -30px;
      bottom: initial !important;
    }

    .buttonArea{
      margin: 1% 8%;
    }
  }

  @media ${screenSize.size650} {
    .headerSecondSlide {
      text-align: center;
    }
    .titlePara {
      margin: 2% 5%;
      display: inline-block;
    }

    .underlinePosition {
      left: 0;
      margin-bottom: 10px;
    }

    .secondHalfSlide .underlinePosition {
      ${"" /* left: 0; */}
    }

    .carousel .carousel-slider {
      height: auto;
    }

    .projectSlide {
      display: flex;
      flex-direction: column;
      height: auto;
      margin-bottom: 15%;
    }

    .titleIntroProjectSlide {
      padding: 9px 1px;
      font-size: 0.8em;
      margin: 1px 8px;
      text-align: center;
    }

    .paraProjectSlide {
      padding: 3px 0px;
      font-size: 1em;
      margin: 1px 2px;
    }

    .firstHalfSlide {
      bottom: 2%;
      padding-bottom: 6%;
    }

    .secondHalfSlide {
      padding: 6% 1% 0px 1%;
    }

    .container {
      display: flex;
      flex-direction: column;
      margin-bottom: 0px;
    }

    .projectName {
      text-align: center;
    }

    .paraProjectText {
      text-align: center;
      padding: 1% 6%;
    }

    .carousel .control-dots {
      top: -4px;
      bottom: initial !important;
    }

    .carousel .control-dots .dot {
      width: 12px;
      height: 12px;
      margin: 15px 8px -15px 8px;
      
    }

    ${'' /* .readmoreBtnPosition {
      text-align: center;
    } */}

    .buttonArea{
      display:flex;
      flex-direction:row;
      width:100%;
      justify-content:center;
      margin:0 0;
      .Button{

      }
    }
    
  }
  
`;

class Projects extends React.Component {
  constructor() {
    super();
  }

  render() {
    return (
      <StyledContainer>
        <div className="container">
          <div className="projectSlideDiv">
            <H3 className="title" color="black" uppercase center>
              our successful projects
            </H3>
            <Underline
              backgroundColor={colors.primaryGreen}
              width="50px"
              height="2px"
            />
            <H1
              className="secondaryTitle"
              color={colors.darkGrey}
              center
              uppercase
            >
              We craft your projects with care
            </H1>
            <div className="paraProjectSlide">
              Our main focus is to make the User Experience very simple and
              easy.
            </div>
            <div className="paraProjectSlide">Simplicity is our Strength.</div>
          </div>

          {/* infiniteLoop={true} autoPlay={true} */}
          <Carousel
          //  infiniteLoop={true}
          //  autoPlay={true}
            showArrows={false}
            showThumbs={false}
            showStatus={false}
          >
            <div>
              <div className="projectSlide">
                <div className="firstHalfSlide">
                  <img src={sliderImage} />
                </div>

                <div className="secondHalfSlide">
                  <div className="headerSecondSlide">
                    <H3
                      className="titlePara"
                      color={colors.darkGrey}
                      uppercase
                      center
                    >
                      E-COMMERCE WEBSITE
                    </H3>
                    <RightUnderline
                      className="underlinePosition"
                      backgroundColor={colors.primaryGreen}
                      width="250px"
                      height="2px"
                    />
                  </div>

                  {/* <h3 className="titleProjects">E-COMMERCE WEBSITE</h3> */}
                  <h2 className="projectName">Nextgen E-Commerce</h2>
                  <div className="paraProjectText">
                    Nextgen UI is all about a smart solution for clothing
                    business. It's a great solution for a e- commerce business.
                    It care about our customer and satisfy their need with easy
                    solutions.Nextgen UI is all about a smart solution for
                    clothing business. It's a great solution for a e-commerce
                    business. It care about our customer and satisfy their need
                    with easy solutions.
                  </div>

                  <div className="buttonArea">
                    {/* <div className="readmoreBtn">Read More</div> */}
                    
                      <Button
                        hoverBackgroundColor="#01c476"
                        hoverBorderColor="#01c476"
                        color="#01c476" 
                        borderColor="#01c476"
                        hoverColor="#fff"
                       
                      >
                        Read More
                      </Button>
                    
                  </div>
                </div>
              </div>
            </div>

            <div>
              <div className="projectSlide">
                <div className="firstHalfSlide">
                  <img src={sliderImage} />
                </div>

                <div className="secondHalfSlide">
                  <div className="headerSecondSlide">
                    <H3
                      className="titlePara"
                      color={colors.darkGrey}
                      uppercase
                      center
                    >
                      E-COMMERCE WEBSITE
                    </H3>
                    <RightUnderline
                      className="underlinePosition"
                      backgroundColor={colors.primaryGreen}
                      width="250px"
                      height="2px"
                    />
                  </div>

                  {/* <h3 className="titleProjects">E-COMMERCE WEBSITE</h3> */}
                  <h2 className="projectName">Nextgen E-Commerce</h2>
                  <div className="paraProjectText">
                    Nextgen UI is all about a smart solution for clothing
                    business. It's a great solution for a e- commerce business.
                    It care about our customer and satisfy their need with easy
                    solutions.Nextgen UI is all about a smart solution for
                    clothing business. It's a great solution for a e-commerce
                    business. It care about our customer and satisfy their need
                    with easy solutions.
                  </div>

                  
                  <div>
                    {/* <div className="readmoreBtn">Read More</div> */}
                    <div>
                      <Button
                        hoverBackgroundColor="#01c476"
                        hoverBorderColor="#01c476"
                        color="#fff"
                      >
                        Read More
                      </Button>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <div>
              <div className="projectSlide">
                <div className="firstHalfSlide">
                  <img src={sliderImage} />
                </div>

                <div className="secondHalfSlide">
                  <div className="headerSecondSlide">
                    <H3
                      className="titlePara"
                      color={colors.darkGrey}
                      uppercase
                      center
                    >
                      E-COMMERCE WEBSITE
                    </H3>
                    <RightUnderline
                      className="underlinePosition"
                      backgroundColor={colors.primaryGreen}
                      width="250px"
                      height="2px"
                    />
                  </div>

                  {/* <h3 className="titleProjects">E-COMMERCE WEBSITE</h3> */}
                  <h2 className="projectName">Nextgen E-Commerce</h2>
                  <div className="paraProjectText">
                    Nextgen UI is all about a smart solution for clothing
                    business. It's a great solution for a e- commerce business.
                    It care about our customer and satisfy their need with easy
                    solutions.Nextgen UI is all about a smart solution for
                    clothing business. It's a great solution for a e-commerce
                    business. It care about our customer and satisfy their need
                    with easy solutions.
                  </div>

                  
                  <div>
                    {/* <div className="readmoreBtn">Read More</div> */}
                    <div>
                      <Button
                        hoverBackgroundColor="#01c476"
                        hoverBorderColor="#01c476"
                        color="#fff"
                      >
                        Read More
                      </Button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </Carousel>
        </div>
      </StyledContainer>
    );
  }
}

export default Projects;
