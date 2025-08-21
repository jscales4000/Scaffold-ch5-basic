# Svelte Scaffolds Repository

A collection of professional Svelte scaffold templates for Crestron CH5 development projects.

## 📁 Repository Structure

```
svelte-scaffolds/
├── scaffolds/           # Main scaffold projects
│   ├── basic-v1/       # Basic scaffold with header/footer/sidebar
│   ├── basic-v2/       # Future enhanced version
│   └── ...             # Additional scaffold variants
├── templates/          # Reusable components and templates
├── docs/              # Documentation and guides
└── tools/             # Build scripts and utilities
```

## 🚀 Available Scaffolds

### Basic-v1
- **Status**: ✅ Production Ready
- **Features**: 
  - Professional header with time display and controls
  - Sidebar navigation with source selection
  - Footer with volume controls and power button
  - Responsive design with TS-1070 scaling support
  - University of Arizona visual standards (customizable)
- **Tech Stack**: Svelte + TypeScript + Tailwind CSS
- **Location**: `scaffolds/basic-v1/`

## 🛠️ Usage

### Quick Start
```bash
# Navigate to desired scaffold
cd scaffolds/basic-v1

# Install dependencies
npm install

# Start development server
npm run dev

# Build for production
npm run build
```

### Creating New Projects
1. Copy desired scaffold folder
2. Rename to your project name
3. Update package.json with project details
4. Customize branding and components as needed

## 📚 Documentation

- [Scaffold Comparison Guide](docs/scaffold-comparison.md)
- [Customization Guide](docs/customization-guide.md)
- [Development Workflow](docs/development-workflow.md)

## 🤝 Contributing

1. Create new scaffold variants in `scaffolds/` directory
2. Follow naming convention: `{type}-v{version}`
3. Include comprehensive README in each scaffold
4. Update main repository documentation

## 📄 License

MIT License - See individual scaffold projects for specific licensing details.