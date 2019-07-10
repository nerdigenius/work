import React from "react";
import styled from "styled-components";
import { screenSize } from "../../constants/screenBreakpoints";
import { colors } from "../../constants/colors";
import ReactDOM from "react-dom";

import H1 from "../ui/H1";
import H2 from "../ui/H2";
import H3 from "../ui/H3";
import Underline from "../ui/Underline";
import RightUnderline from "../ui/RightUnderline";
import Button from "../ui/Button";
import feat1 from "../../static/Icon/feature1.png";
import feat2 from "../../static/Icon/feature2.png";
import feat3 from "../../static/Icon/feature3.png";
import feat4 from "../../static/Icon/feature4.png";
const StyledContainer = styled.div`
  .projectSlide {
    display: flex;

    padding: 3% 0;
  }
  .firstHalfSlide {
    flex-basis: 50%;
  }

  .container {
    margin-bottom: 80px;
  }
  .secondHalfSlide {
    flex-basis: 50%;
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

  .left-image-f1 {
    max-width: 100%;
    width: 75%;
  }

  .primary-style {
    background-color: #f5f5f5;
  }

  .secondary-style {
    background-color: #fff;
  }

  .secondary-style .secondHalfSlide {
    text-align: left;
  }

  .secondary-style .titlePara {
    text-align: left;
  }

  @media ${screenSize.size960} {
    .projectSlide {
      height: auto;
    }

    .secondHalfSlide {
      padding: 0 4% 0px 4%;
    }

    .primary-style .buttonPosition {
      text-align: right;
    }
  }

  @media ${screenSize.size650} {
    .headerSecondSlide {
      text-align: center;
    }
    .titlePara {
      ${"" /* margin: 2% 11% */}
      display: block;
    }

    .underlinePosition {
      left: 0;
      margin-bottom: 10px;
    }

    ${'' /* .secondHalfSlide .underlinePosition {
     
    } */}

    .projectSlide {
      display: flex;
      flex-direction: column-reverse;
      height: auto;

      margin: 4% 0 4% 0;
    }

    .firstHalfSlide {
      bottom: 2%;
      padding-bottom: 6%;
      text-align: center;
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

    .buttonArea {
      margin: 6% 10%;
    }

    .feature-position {
      order: 2;
    }

    .button-width {
      min-width: 62%;
    }

    .feature-position {
      order: 2;
    }

    .secondHalfSlide .titlePara {
      text-align: center;
    }

    .buttonArea {
      margin: 5% 8%;
      .buttonPosition {
        text-align: center;
      }
    }
  }
`;

class Features extends React.Component {
  constructor() {
    super();
  }

  render() {
    return (
      <StyledContainer>
        <div className="container">
          <div className="wrapper">
            <div>
              <div className="projectSlide primary-style">
                <div className="firstHalfSlide feature-position">
                  <img className="left-image-f1" src={feat1} />
                </div>

                <div className="secondHalfSlide primary-style">
                  <div className="headerSecondSlide">
                    <H3
                      className="titlePara"
                      color={colors.darkGrey}
                      uppercase
                      center
                    >
                      Coding is life
                    </H3>
                    <RightUnderline
                      className="underlinePosition"
                      backgroundColor={colors.primaryGreen}
                      width="250px"
                      height="2px"
                    />
                  </div>

                  {/* <h3 className="titleProjects">E-COMMERCE WEBSITE</h3> */}
                  <h2 className="projectName">Feel our structural coding</h2>
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
                    <div className="buttonPosition">
                      <Button
                        className="button-width"
                        hoverBackgroundColor="#01c476"
                        hoverBorderColor="#01c476"
                        color="#01c476"
                        borderColor="#01c476"
                        hoverColor="#fff"
                      >
                        SEE OUR PORTFOLIO
                      </Button>
                    </div>
                  </div>
                </div>
              </div>

              <div className="projectSlide secondary-style">
                <div className="secondHalfSlide feature-position">
                  <div className="headerSecondSlide">
                    <H3
                      className="titlePara"
                      color={colors.darkGrey}
                      uppercase
                      center
                    >
                      Web Development
                    </H3>
                    <RightUnderline
                      className="underlinePosition"
                      backgroundColor={colors.primaryGreen}
                      width="250px"
                      height="2px"
                    />
                  </div>

                  {/* <h3 className="titleProjects">E-COMMERCE WEBSITE</h3> */}
                  <h2 className="projectName">Feel our responsive design</h2>
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
                    <div className="buttonPosition">
                      <Button
                        className="button-width"
                        hoverBackgroundColor="#01c476"
                        hoverBorderColor="#01c476"
                        color="#01c476"
                        borderColor="#01c476"
                        hoverColor="#fff"
                      >
                        SEE OUR PORTFOLIO
                      </Button>
                    </div>
                  </div>
                </div>

                <div className="firstHalfSlide feature-position">
                  <img className="left-image-f1" src={feat2} />
                </div>
              </div>
              <div className="projectSlide primary-style">
                <div className="firstHalfSlide feature-position">
                  <img className="left-image-f1" src={feat3} />
                </div>

                <div className="secondHalfSlide primary-style">
                  <div className="headerSecondSlide">
                    <H3
                      className="titlePara"
                      color={colors.darkGrey}
                      uppercase
                      center
                    >
                      Coding
                    </H3>
                    <RightUnderline
                      className="underlinePosition"
                      backgroundColor={colors.primaryGreen}
                      width="250px"
                      height="2px"
                    />
                  </div>

                  {/* <h3 className="titleProjects">E-COMMERCE WEBSITE</h3> */}
                  <h2 className="projectName">Feel our structural coding</h2>
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
                    <div className="buttonPosition">
                      <Button
                        className="button-width"
                        hoverBackgroundColor="#01c476"
                        hoverBorderColor="#01c476"
                        color="#01c476"
                        borderColor="#01c476"
                        hoverColor="#fff"
                      >
                        SEE OUR PORTFOLIO
                      </Button>
                    </div>
                  </div>
                </div>
              </div>

              <div className="projectSlide secondary-style">
                <div className="secondHalfSlide feature-position">
                  <div className="headerSecondSlide">
                    <H3
                      className="titlePara"
                      color={colors.darkGrey}
                      uppercase
                      center
                    >
                      Web Development
                    </H3>
                    <RightUnderline
                      className="underlinePosition"
                      backgroundColor={colors.primaryGreen}
                      width="250px"
                      height="2px"
                    />
                  </div>

                  {/* <h3 className="titleProjects">E-COMMERCE WEBSITE</h3> */}
                  <h2 className="projectName">Feel our responsive design</h2>
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
                    <div className="buttonPosition">
                      <Button
                        className="button-width"
                        hoverBackgroundColor="#01c476"
                        hoverBorderColor="#01c476"
                        color="#01c476"
                        borderColor="#01c476"
                        hoverColor="#fff"
                      >
                        SEE OUR PORTFOLIO
                      </Button>
                    </div>
                  </div>
                </div>

                <div className="firstHalfSlide feature-position">
                  <img className="left-image-f1" src={feat4} />
                </div>
              </div>
            </div>
          </div>
        </div>
      </StyledContainer>
    );
  }
}

export default Features;
