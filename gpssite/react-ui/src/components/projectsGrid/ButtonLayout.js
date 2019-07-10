import React, { Component } from 'react';
import Shadowbtn from "../ui/largeShadowbtn";
import styled from "styled-components";
import { colors } from '../../constants/colors';

const StyledContainer = styled.div`

`



 class ButtonLayout extends Component {
    render()
    {
        return(
            <StyledContainer>
                <div className="buttonlayout">
                    <Shadowbtn height="15%" width="24%" Border="1px solid white" className="mediamargin" Font="poppins" Color={colors.primaryGreen}>
                        <img className="imagesize" src={require('../../static/Icon/allCatagories.png')} />
                        <span>All Catagories</span>

                    </Shadowbtn>
                    <Shadowbtn height="15%" width="24%" Border="1px solid white" className="mediamargin" Font="poppins" Color={colors.primaryGreen}>
                        <img className="imagesize" src={require('../../static/Icon/webdev.png')} />
                        <span>Web Development</span>

                    </Shadowbtn>
                    <Shadowbtn height="15%" width="24%" Border="1px solid white" className="mediamargin" Font="poppins" Color={colors.primaryGreen}>
                        <img className="imagesize" src={require('../../static/Icon/mobile.png')} />
                        <span>Mobile Development</span>

                    </Shadowbtn>
                    <Shadowbtn height="15%" width="24%" Border="1px solid white" className="mediamargin" Font="poppins" Color={colors.primaryGreen}>
                        <img className="imagesize" src={require('../../static/Icon/digi.png')} />
                        <span>Digital Marketing</span>

                    </Shadowbtn>

                </div>

            </StyledContainer>
            
        )
    }
   
}
export default ButtonLayout;