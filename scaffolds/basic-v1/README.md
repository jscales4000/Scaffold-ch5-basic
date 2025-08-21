# Basic-v1 Svelte Scaffold

Professional Svelte scaffold template for Crestron CH5 touch panel development.

## ✨ Features

### Layout Components
- **Header**: Fixed 64px height with time display, help, and settings buttons
- **Sidebar**: Source selection with hover and active states
- **Main Content**: Responsive content area with professional branding
- **Footer**: 100px height with power button and volume controls

### Visual Standards
- **Professional Styling**: Shadowbox components with depth and lighting
- **Responsive Design**: TS-770 (1280x800) and TS-1070 (1920x1200) support
- **UA Brand Colors**: University of Arizona color scheme (customizable)
- **Font System**: Segoe UI with cross-platform fallbacks

### Technical Features
- **TypeScript**: Full type safety
- **Tailwind CSS**: Utility-first styling
- **Component Architecture**: Reusable shared components
- **Build System**: Vite for fast development and builds

## 🚀 Quick Start

```bash
# Install dependencies
npm install

# Start development server
npm run dev

# Build for production
npm run build
```

## 📱 Responsive Design

### TS-770 (1280x800)
- Base font size: 16.8px
- Standard component scaling
- Optimized for standard touch panels

### TS-1070 (1920x1200)
- Automatic 1.5x scaling
- Maintains aspect ratio
- Enhanced for larger displays

## 🎨 Customization

### Branding
1. Update colors in `src/app.css`:
   ```css
   :root {
     --color-primary: #0A1C3A;
     --color-accent: #AB0520;
   }
   ```

2. Replace logo files in `public/images/`

3. Update room name in `src/App.svelte`

### Layout Modifications
- **Header**: Edit `src/components/shared/Header.svelte`
- **Sidebar**: Modify `src/components/shared/Sidebar.svelte`  
- **Footer**: Customize `src/components/shared/Footer.svelte`

## 📁 Project Structure

```
basic-v1/
├── src/
│   ├── components/shared/    # Reusable components
│   ├── lib/styles/          # Shared styling
│   ├── App.svelte           # Main application
│   └── main.ts              # Entry point
├── public/
│   └── images/              # Static assets
└── package.json             # Dependencies
```

## 🛠️ Development Workflow

### Adding New Sources
1. Update source array in `src/App.svelte`
2. Add icons and labels
3. Implement source-specific logic

### Styling Guidelines
- Use shadowbox utility classes for containers
- Follow existing spacing patterns (0.5rem, 1rem)
- Maintain color consistency with CSS variables

### Component Development
- Keep components in `src/components/shared/`
- Use TypeScript for all new components
- Follow existing naming conventions

## 📊 Current Status

- ✅ **Header**: Complete with time and controls
- ✅ **Footer**: Volume controls properly positioned
- ✅ **Sidebar**: Source selection with active states
- ✅ **Responsive**: TS-770/TS-1070 scaling
- ✅ **Build System**: Production ready

## 🔄 Version History

### v1.0.0 (August 21, 2025)
- Initial release
- Complete layout structure
- Professional styling system
- Responsive design implementation