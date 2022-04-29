import React from 'react';
import Grid from '@mui/material/Grid';

import { ComponentStory, ComponentMeta } from '@storybook/react'

import { ReleaseCard } from './ReleaseCard'

export default {
    title: 'Release Card',
    component: ReleaseCard
} as ComponentMeta<typeof ReleaseCard>;

const Template: ComponentStory<typeof ReleaseCard> = (args) => <Grid container> <Grid item><ReleaseCard {...args} /></Grid></Grid>

export const Primary = Template.bind({});
Primary.args = { versionNumber: "1.0.0" }
// Primary.args = { name: "1.0.0", lastUpdate: "Updated 5 days ago" }