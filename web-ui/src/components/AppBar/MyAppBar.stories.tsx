import { ComponentStory, ComponentMeta } from '@storybook/react'

import { MyAppBar } from './MyAppBar'

export default {
    title: 'App Bar',
    component: MyAppBar
} as ComponentMeta<typeof MyAppBar>;

const Template: ComponentStory<typeof MyAppBar> = (args) => <MyAppBar />

export const Primary = Template.bind({});
Primary.args = {}