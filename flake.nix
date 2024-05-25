{
  description = "rimworld mods";

  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/nixos-23.11";
    flakelight.url = "github:nix-community/flakelight";
  };

  outputs = { self, flakelight, ... }@inputs:
    flakelight ./. {
      inherit inputs;

      devShell.packages = pkgs: with pkgs;[
        libxml2
        lemminx # lsp

        # mono stuff
        ilspycmd
        avalonia-ilspy
        msbuild
        mono
	omnisharp-roslyn # lsp
	dotnet-sdk
      ];
      devShell.env= pkgs: {
	      DOTNET_ROOT = "${pkgs.dotnet-sdk}";
	};
    };
}

