import React from "react";
import styled from "styled-components";
import Buttons from "../ui/ButtonClicked";
import ButtonHover from "../ui/Button";
import Para from "../ui/ParaBlogs";
import H3 from "../ui/H3";
import Underline from "../ui/Underline";
import Button from "../ui/Button";
import { colors } from "../../constants/colors";

import BlogImage1 from "../../static/Icon/blog1.png";
import BlogImage2 from "../../static/Icon/blog2.png";

import { screenSize } from "../../constants/screenBreakpoints";

const StyledContainer = styled.div`
  .container {
    margin-bottom: 80px;
  }

  .wrapper {
    margin: 3% 1% 3% 1%;
  }

  .buttonArea {
    display: flex;
    ${"" /* justify-content: space-around; */}
    flex-wrap: wrap;
    margin: 2%;
    padding: 0 8% 0 8%;
  }

  .btn-container {
    ${"" /* flex-basis: 13%; */}

    margin-right: 16px;

    margin-bottom: 16px;
  }

  .btn-container div {
    padding: 1em 1.2em;
  }
  .blog-section {
    display: flex;
  }

  .blog-image {
    flex-basis: 50%;
  }

  ${"" /* .img-block:hover {


    opacity: 0.3;
    filter: alpha(opacity=30);
  } */}

  .blog-text {
    flex-basis: 50%;
    margin: auto 0;
    padding: 1% 2%;

    .header-blog {
      color: #4a4a4a;
      font-family: "Poppins", sans-serif;
      font-size: 28px;
      font-weight: 700;
      line-height: 31px;
      margin-bottom: 3%;
    }

    .timeline {
      font-family: "Roboto", sans-serif;
      font-size: 14px;
      font-weight: 400;
      line-height: 15px;
      color: #c4c4c4;
      margin-bottom: 3%;
    }

    .paragraph {
      margin-bottom: 5%;
    }

    .end-text {
      font-family: "Roboto", sans-serif;
      font-size: 14px;
      font-weight: 400;
      letter-spacing: 1.41px;
      line-height: 15px;
      text-transform: uppercase;
      color: #c4c4c4;
    }
  }

  .blog-text h3 {
    ${"" /* line-height: 50px;
      color:  */}
  }

  .blogs-area {
    padding: 2% 10% 5% 10%;
    ${"" /* padding: 2% 12%; */}

    .blog-section {
      ${"" /* padding: 1% 1%; */}
      ${"" /* box-shadow: 0 1px 6px 0 rgba(0,0,0,0.2), 0 2px 2px 0 rgba(0, 0, 0, 0); */}
      -webkit-box-shadow: 5px -1px 18px -8px rgba(107,101,107,0.79);
      -moz-box-shadow: 5px -1px 18px -8px rgba(107, 101, 107, 0.79);
      box-shadow: 5px -1px 18px -8px rgba(107, 101, 107, 0.79);
      .blog-image {
        ${"" /* padding: 2px 5px; */}
      }
    }
  }

  .end-blog-text {
    color: #01c476;
    text-align: center;
    padding-bottom: 12px;
  }

  .content {
    position: relative;
    ${"" /* width: 90%; */}
    ${"" /* max-width: 400px; */}
    margin: auto;
    overflow: hidden;
  }
  .buttonHover:hover{
    color:white;
    background-color:${colors.primaryGreen};

  }

  .content .content-overlay {
    background: rgba(0, 0, 0, 0.7);
    position: absolute;
    height: 99%;
    width: 100%;
    left: 0;
    top: 0;
    bottom: 0;
    right: 0;
    opacity: 0;
    -webkit-transition: all 0.4s ease-in-out 0s;
    -moz-transition: all 0.4s ease-in-out 0s;
    transition: all 0.4s ease-in-out 0s;
  }

  .content:hover .content-overlay {
    opacity: 1;
  }

  .content-image {
    width: 100%;
  }

  .content-details {
    position: absolute;
    text-align: center;
    padding-left: 1em;
    padding-right: 1em;
    width: 100%;
    top: 50%;
    left: 50%;
    opacity: 0;
    -webkit-transform: translate(-50%, -50%);
    -moz-transform: translate(-50%, -50%);
    transform: translate(-50%, -50%);
    -webkit-transition: all 0.3s ease-in-out 0s;
    -moz-transition: all 0.3s ease-in-out 0s;
    transition: all 0.3s ease-in-out 0s;
  }

  .content:hover .content-details {
    top: 50%;
    left: 50%;
    opacity: 1;
  }

  .content-details h3 {
    color: #fafafa;
    font-family: Roboto;
    font-size: 40px;
    font-weight: 300;
    margin-bottom: 15px;
  }

  .content-details p {
    color: #fafafa;
    font-family: Roboto;
    font-size: 14px;
    font-weight: 400;
    line-height: 22px;
    margin-bottom: 25px;
    padding: 3% 23%;
  }

  .content-details .hover-text-div {
    margin-bottom: 15px;
  }

  .fadeIn-bottom {
    top: 80%;
  }

  .fadeIn-top {
    top: 20%;
  }

  .fadeIn-left {
    left: 20%;
  }

  .fadeIn-right {
    left: 80%;
  }

  .blogs-area .blog-section:last-child {
    -webkit-box-shadow: 5px 10px 18px -8px rgba(107, 101, 107, 0.79);
    -moz-box-shadow: 5px 10px 18px -8px rgba(107, 101, 107, 0.79);
    box-shadow: 5px 10px 18px -8px rgba(107, 101, 107, 0.79);
  }

  .blog-text .header-blog a:link {
    text-decoration: none;
    color:black;
  }

  .blog-text .header-blog a:visited {
    color: #4a4a4a;
  }

  .blog-text .header-blog a:hover {
    color: ${colors.primaryGreen};
  }

  .blog-text .header-blog a:active {
    text-decoration: none;
  }

  .horizontal-line {
    opacity: 0.2;
    width: 70%;
  }

.para-gap-hr-line{
  margin-bottom: 8px;
}
  

  @media ${screenSize.size650} {
    .blog-wrapper{
      padding-left:1px;
    }
    .blogs-area .blog-section{
    -webkit-box-shadow: 5px 10px 18px -8px rgba(107, 101, 107, 0.79);
    -moz-box-shadow: 5px 10px 18px -8px rgba(107, 101, 107, 0.79);
    box-shadow: 5px 10px 18px -8px rgba(107, 101, 107, 0.79);
  }
    .btn-container .btn-width {
      width: auto;
    }

    ${"" /* .blogs-area .blog-section {
      display: flex;
      flex-direction: column;
      margin-bottom: 15px;
    } */}

    .primary {
      flex-direction: column;
    }
    .reverse {
      flex-direction: column-reverse;
    }

    .blogs-area .blog-section {
      margin-bottom: 50px;
    }

    .blog-text {
      margin-bottom: 10px;
    }

    .blog-text .header-blog {
      margin-bottom: 27px;
    }
    .header-blog a{
      font-size:18px;
      
    }
  }
`;

class BlogBody extends React.Component {
  constructor() {
    super();
  }

  render() {
    return (
      <StyledContainer>
        <div className="container">
          <div className="wrapper">
            <div className="buttonArea">
              <div className="btn-container ">
                <Buttons
                  className="btn-width"
                  hoverBackgroundColor="#01c476"
                  hoverBorderColor="#01c476"
                  color="#01c476"
                  borderColor="#01c476"
                  hoverColor="#fff"
                >
                  All Categories
                </Buttons>
              </div>

              <div className="btn-container">
                <Buttons
                  className="btn-width"
                  hoverBackgroundColor="#01c476"
                  hoverBorderColor="#01c476"
                  color="#01c476"
                  borderColor="#01c476"
                  hoverColor="#fff"
                >
                  Web Development
                </Buttons>
              </div>

              <div className="btn-container">
                <Buttons
                  className="btn-width"
                  hoverBackgroundColor="#01c476"
                  hoverBorderColor="#01c476"
                  color="#01c476"
                  borderColor="#01c476"
                  hoverColor="#fff"
                >
                  Mobile Development
                </Buttons>
              </div>

              <div className="btn-container">
                <Buttons
                  className="btn-width"
                  hoverBackgroundColor="#01c476"
                  hoverBorderColor="#01c476"
                  color="#01c476"
                  borderColor="#01c476"
                  hoverColor="#fff"
                >
                  Digital Marketing
                </Buttons>
              </div>

              <div className="btn-container">
                <Buttons
                  className="btn-width"
                  hoverBackgroundColor="#01c476"
                  hoverBorderColor="#01c476"
                  color="#01c476"
                  borderColor="#01c476"
                  hoverColor="#fff"
                >
                  SEO
                </Buttons>
              </div>

              <div className="btn-container">
                <Buttons
                  className="btn-width"
                  hoverBackgroundColor="#01c476"
                  hoverBorderColor="#01c476"
                  color="#01c476"
                  borderColor="#01c476"
                  hoverColor="#fff"
                >
                  PYTHON
                </Buttons>
              </div>

              <div className="btn-container">
                <Buttons
                  className="btn-width"
                  hoverBackgroundColor="#01c476"
                  hoverBorderColor="#01c476"
                  color="#01c476"
                  borderColor="#01c476"
                  hoverColor="#fff"
                >
                  RND
                </Buttons>
              </div>
            </div>

            <div className="blogs-area">
              <div className="blog-section primary">
                <div className="blog-image">
                  {/* <img className="img-block" src={BlogImage1} /> */}
                  <div className="content">
                    <a
                      href="https://unsplash.com/photos/HkTMcmlMOUQ"
                      target="_blank"
                    >
                      <div className="content-overlay" />
                      <img className="content-image" src={BlogImage1} />
                      <div className="content-details fadeIn-top">
                        <div className="hover-text-div">
                          <h3>Projecturf</h3>
                          <Underline
                            backgroundColor={colors.primaryGreen}
                            width="50px"
                            height="2px"
                          />
                        </div>

                        <div>
                          <p className="">
                            This project is about making life easier for
                            transport and food stuff .. more stuff easier to do
                            have to do more and easy to find car initially
                            ingredients
                          </p>
                        </div>
                        {/* <h3>This is a title</h3> */}

                        <div>
                          <ButtonHover
                            hoverBackgroundColor="#01c476"
                            hoverBorderColor="#01c476"
                            color="#01c476"
                            borderColor="#01c476"
                            hoverColor="#fff"
                            borderRadius="5px"
                          >
                            SEE DETAILS
                          </ButtonHover>
                        </div>

                        {/* <p>This is a short description</p> */}
                      </div>
                    </a>
                  </div>
                </div>

                <div className="blog-text">
                  <div className="blog-wrapper">
                    <div>
                      <div className="timeline">
                        by Jason Lee{" "}
                        <span>
                          <i class="far fa-clock" />
                        </span>{" "}
                        June 26 | 10 minutes read
                      </div>
                    </div>
                    <div className="header-blog">
                      {/* <H1 lineHeight="50px"> */}
                      <a
                        href="https://unsplash.com/photos/HkTMcmlMOUQ"
                        target="_blank"
                      >
                        How to Choose a Ruby on Rails Consulting Company and
                        Perfect RoR Developers [Quick Guide]
                      </a>

                      {/* </H1> */}
                    </div>
                    <div className="paragraph">
                      <Para className="para-gap-hr-line">
                        Hello again! It’s the third time I get to share with you
                        some tips and tricks on time management and I am certain
                        that by now you’ve had enough ...
                      </Para>

                      <hr className="horizontal-line" />
                    </div>

                    <div className="end-text">RUBY, CONSULTING</div>
                  </div>
                </div>
              </div>

              <div className="blog-section reverse">
                <div className="blog-text">
                  <div className="blog-wrapper">
                    <div>
                      <div className="timeline">
                        by Jason Lee June 26 | 10 minutes read
                      </div>
                    </div>
                    <div className="header-blog">
                      {/* <H1 lineHeight="50px"> */}
                      <a
                        href="https://unsplash.com/photos/HkTMcmlMOUQ"
                        target="_blank"
                      >
                        How to Choose a Ruby on Rails Consulting Company and
                        Perfect RoR Developers [Quick Guide]
                      </a>

                      {/* </H1> */}
                    </div>
                    <div className="paragraph">
                    <Para className="para-gap-hr-line">
                        Hello again! It’s the third time I get to share with you
                        some tips and tricks on time management and I am certain
                        that by now you’ve had enough ...
                      </Para>

                      <hr className="horizontal-line" />
                    </div>

                    <div className="end-text">RUBY, CONSULTING</div>
                  </div>
                </div>

                <div className="blog-image ">
                  {/* <img className="img-block" src={BlogImage1} /> */}
                  <div className="content">
                    <a
                      href="https://unsplash.com/photos/HkTMcmlMOUQ"
                      target="_blank"
                    >
                      <div className="content-overlay" />
                      <img className="content-image" src={BlogImage1} />
                      <div className="content-details fadeIn-left">
                        <div className="hover-text-div">
                          <h3>Projecturf</h3>
                          <Underline
                            backgroundColor={colors.primaryGreen}
                            width="50px"
                            height="2px"
                          />
                        </div>

                        <div>
                          <p>
                            This project is about making life easier for
                            transport and food stuff .. more stuff easier to do
                            have to do more and easy to find car initially
                            ingredients
                          </p>
                        </div>
                        {/* <h3>This is a title</h3> */}

                        <div>
                          <ButtonHover
                            hoverBackgroundColor="#01c476"
                            hoverBorderColor="#01c476"
                            color="#01c476"
                            borderColor="#01c476"
                            hoverColor="#fff"
                            borderRadius="5px"
                          >
                            SEE DETAILS
                          </ButtonHover>
                        </div>

                        {/* <p>This is a short description</p> */}
                      </div>
                    </a>
                  </div>
                </div>
              </div>

              <div className="blog-section primary">
                <div className="blog-image">
                  {/* <img className="img-block" src={BlogImage1} /> */}
                  <div className="content">
                    <a
                      href="https://unsplash.com/photos/HkTMcmlMOUQ"
                      target="_blank"
                    >
                      <div className="content-overlay" />
                      <img className="content-image" src={BlogImage1} />
                      <div className="content-details fadeIn-right">
                        <div className="hover-text-div">
                          <h3>Projecturf</h3>
                          <Underline
                            backgroundColor={colors.primaryGreen}
                            width="50px"
                            height="2px"
                          />
                        </div>

                        <div>
                          <p>
                            This project is about making life easier for
                            transport and food stuff .. more stuff easier to do
                            have to do more and easy to find car initially
                            ingredients
                          </p>
                        </div>
                        {/* <h3>This is a title</h3> */}

                        <div>
                          <ButtonHover
                            hoverBackgroundColor="#01c476"
                            hoverBorderColor="#01c476"
                            color="#01c476"
                            borderColor="#01c476"
                            hoverColor="#fff"
                            borderRadius="5px"
                          >
                            SEE DETAILS
                          </ButtonHover>
                        </div>

                        {/* <p>This is a short description</p> */}
                      </div>
                    </a>
                  </div>
                </div>

                <div className="blog-text">
                  <div className="blog-wrapper">
                    <div>
                      <div className="timeline">
                        by Jason Lee June 26 | 10 minutes read
                      </div>
                    </div>
                    <div className="header-blog">
                      {/* <H1 lineHeight="50px"> */}
                      <a
                        href="https://unsplash.com/photos/HkTMcmlMOUQ"
                        target="_blank"
                      >
                        How to Choose a Ruby on Rails Consulting Company and
                        Perfect RoR Developers [Quick Guide]
                      </a>

                      {/* </H1> */}
                    </div>
                    <div className="paragraph">
                    <Para className="para-gap-hr-line">
                        Hello again! It’s the third time I get to share with you
                        some tips and tricks on time management and I am certain
                        that by now you’ve had enough ...
                      </Para>
                      <hr className="horizontal-line" />
                    </div>

                    <div className="end-text">RUBY, CONSULTING</div>
                  </div>
                </div>
              </div>

              <div className="blog-section reverse">
                <div className="blog-text">
                  <div className="blog-wrapper">
                    <div>
                      <div className="timeline">
                        by Jason Lee June 26 | 10 minutes read
                      </div>
                    </div>
                    <div className="header-blog">
                      {/* <H1 lineHeight="50px"> */}
                      <a
                        href="https://unsplash.com/photos/HkTMcmlMOUQ"
                        target="_blank"
                      >
                        How to Choose a Ruby on Rails Consulting Company and
                        Perfect RoR Developers [Quick Guide]
                      </a>

                      {/* </H1> */}
                    </div>
                    <div className="paragraph">
                    <Para className="para-gap-hr-line">
                        Hello again! It’s the third time I get to share with you
                        some tips and tricks on time management and I am certain
                        that by now you’ve had enough ...
                      </Para>
                      <hr className="horizontal-line" />
                    </div>

                    <div className="end-text">RUBY, CONSULTING</div>
                  </div>
                </div>

                <div className="blog-image">
                  {/* <img className="img-block" src={BlogImage1} /> */}
                  <div className="content">
                    <a
                      href="https://unsplash.com/photos/HkTMcmlMOUQ"
                      target="_blank"
                    >
                      <div className="content-overlay" />
                      <img className="content-image" src={BlogImage1} />
                      <div className="content-details fadeIn-bottom">
                        <div className="hover-text-div">
                          <h3>Projecturf</h3>
                          <Underline
                            backgroundColor={colors.primaryGreen}
                            width="50px"
                            height="2px"
                          />
                        </div>

                        <div>
                          <p>
                            This project is about making life easier for
                            transport and food stuff .. more stuff easier to do
                            have to do more and easy to find car initially
                            ingredients
                          </p>
                        </div>
                        {/* <h3>This is a title</h3> */}

                        <div>
                          <ButtonHover
                            hoverBackgroundColor="#01c476"
                            hoverBorderColor="#01c476"
                            color="#01c476"
                            borderColor="#01c476"
                            hoverColor="#fff"
                            borderRadius="5px"
                          >
                            SEE DETAILS
                          </ButtonHover>
                        </div>

                        {/* <p>This is a short description</p> */}
                      </div>
                    </a>
                  </div>
                </div>
              </div>
            </div>

            <div className="end-blog-text">
              <Button className="buttonHover">VIEW MORE</Button>
              
            </div>
          </div>
        </div>
      </StyledContainer>
    );
  }
}

export default BlogBody;
